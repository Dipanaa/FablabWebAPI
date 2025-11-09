using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.UsuariosDtos;
using FablabWebAPI.Entities;
using FablabWebAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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
        private const string contenedor = "Usuarios";

        public UsuariosController(ApplicationDbContext context, IMapper autoMapper, UserManager<Usuario> userManager, IAlmacenadorArchivos almacenadorArchivos) {

            this.context = context;
            this.mapper = autoMapper;
            this.userManager = userManager;
            this.almacenadorArchivos = almacenadorArchivos;
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

        //Este Post es con foto, verificar su uso en movil, ESTE ENDPOINT ES SOLO DE TESTING
        [HttpPost]
        public async Task<ActionResult> PostUsuarioFoto([FromForm] UsuarioCreateDto usuarioCreateDto)
        {
            var usuarioMappeado = mapper.Map<Usuario>(usuarioCreateDto);

            if(usuarioCreateDto.ImgUrl is not null)
            {
                var urlImg = await almacenadorArchivos.Agregar(contenedor, usuarioCreateDto.ImgUrl);
                usuarioMappeado.ImgUrl = urlImg;
            }

            context.Add(usuarioMappeado);
            await this.context.SaveChangesAsync();
            return Ok();
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

            var usuario = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario is null)
            {
                return NotFound();
            }

            if (usuario.RolId! != 1) //Es administrador
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
