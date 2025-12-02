using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.UsuariosDtos;
using FablabWebAPI.Entities;
using FablabWebAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Claims;
using System.Text.Json;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        private readonly UserManager<Usuario> userManager;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly ILogger<UsuariosController> logger;
        private readonly IServicioUsuarios servicioUsuarios;
        private const string contenedor = "usuarios";

        public UsuariosController(ApplicationDbContext context, IMapper autoMapper, UserManager<Usuario> userManager,
            IAlmacenadorArchivos almacenadorArchivos, ILogger<UsuariosController> logger, IServicioUsuarios servicioUsuarios) {

            this.context = context;
            this.mapper = autoMapper;
            this.userManager = userManager;
            this.almacenadorArchivos = almacenadorArchivos;
            this.logger = logger;
            this.servicioUsuarios = servicioUsuarios;
        }

        
        [HttpGet]
        public async Task<IEnumerable<UsuarioDto>> Get()
        {
            var listaUsuarios = await this.context.Usuario
                .Include(x => x.Laboratorio)
                .Include(x => x.Rol)
                .Include(x => x.Proyectos)
                .ToListAsync();

            var listaUsuariosDto = mapper.Map<List<UsuarioDto>>(listaUsuarios);
            return listaUsuariosDto;

        }

        [HttpPost]
        public async Task<ActionResult> Post(Usuario usuario)
        {
            context.Add(usuario);
            await this.context.SaveChangesAsync();
            return Ok();
        }

        //Este Post es con foto, verificar su uso en movil
        [HttpPut("perfil/{id:int}")]
        public async Task<ActionResult> PostUsuarioFoto([FromForm] UsuarioFotoMTF usuarioFotoMTF,int id)
        {
            var usuarioDeserializado = JsonSerializer.Deserialize<UsuarioPerfilPutDto>(usuarioFotoMTF.DataUser, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if(usuarioDeserializado is null)
            {
                return BadRequest();
            }

            var usuario = await userManager.FindByIdAsync(id.ToString());

            if(usuario == null)
            {
                return NotFound();
            }

            mapper.Map(usuarioDeserializado, usuario);

            if (usuarioFotoMTF.ImgUrl is not null)
            {                
                logger.LogWarning("la informacion de la imagen esta siendo editada");
                var urlImg = await almacenadorArchivos.Editar(usuario.ImgUrl,contenedor, usuarioFotoMTF.ImgUrl);
                usuario.ImgUrl = urlImg;
            }

            var resultadoActualizacion = await userManager.UpdateAsync(usuario);

            if (resultadoActualizacion.Succeeded) {

                return Ok();
            
            }

            return BadRequest();
        }



        [HttpPut("{id:int}")] 
        public async Task<ActionResult> Put(int id, UsuarioPutDto usuarioDto)
        {

            var usuarioEncontrado = await context.Usuario.FirstOrDefaultAsync(x => x.Id == id);

            if (usuarioEncontrado is null)
            {
                return NotFound();
            }

            mapper.Map(usuarioDto, usuarioEncontrado);
            await userManager.UpdateAsync(usuarioEncontrado);
        
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            //Verificar usuario activo
            var usuarioActivo = await servicioUsuarios.ObtenerUsuario();


            //Usuario a eliminar
            var usuario = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario is null)
            {
                return NotFound();
            }

            if (usuarioActivo.RolId! != 1) //Es administrador
            {
                return Problem(
                    title: "Permiso denegado",
                    detail: "No cuentas con los permisos para realizar esta accion",
                    statusCode: StatusCodes.Status403Forbidden
                    );
            }

            await userManager.DeleteAsync(usuario);
            return Ok();

        }

    }
}
