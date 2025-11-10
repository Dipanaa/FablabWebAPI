using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.ProyectosDtos;
using FablabWebAPI.DTOs.UsuarioProyectoDtos;
using FablabWebAPI.DTOs.UsuariosDtos;
using FablabWebAPI.Entities;
using FablabWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Text.Json;
using System.Threading.Tasks;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/proyectos")]
    public class ProyectosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private const string contenedor = "proyectos";

        public ProyectosController(ApplicationDbContext context, IMapper autoMapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = autoMapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }


        //Get de todos los proyectos con sus usuarios
        [HttpGet]
        public async Task<IEnumerable<ProyectoConUsuariosDtos>> Get()
        {
            var proyectos = await this.context.Proyectos.Include(pro => pro.Usuarios).ThenInclude(up => up.Usuario).ToListAsync();

            var proyectosDto = mapper.Map<IEnumerable<ProyectoConUsuariosDtos>>(proyectos);
            return proyectosDto;
        }


        //Get de proyecto en base a un id de usuario
        [HttpGet("{id:int}")]
        public async Task<IEnumerable<ProyectosDto>> Get(int id) 
        {
            var proyectosDeUsuario = await context.UsuarioProyecto.Where(up => up.UsuarioId == id).Select(up => up.Proyectos).ToListAsync();

            var proyectosDto = mapper.Map<IEnumerable<ProyectosDto>>(proyectosDeUsuario);

            return proyectosDto;
        }


        //Proyectos con coleccion
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProyectoFotoMTF proyectosFotoMTFDto)
        {
            var proyectoDeserializado = JsonSerializer.Deserialize<CreateProyectosDto>(proyectosFotoMTFDto.DataProject, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if(proyectoDeserializado is null)
            {
                return BadRequest();
            }

            if (proyectoDeserializado.Ids is null || proyectoDeserializado.Ids.Count() == 0)
            {
                return BadRequest();
            }

            var usuariosExistentes = await context.Usuario.Where(user => proyectoDeserializado.Ids.Contains(user.Id)).Select(user => user.Id).ToListAsync();

            if(usuariosExistentes.Count() != proyectoDeserializado.Ids.Count())
            {
                //TODO:poner error de validacion 
                return NotFound();
            }

            var proyectoUsuarioMappeado = mapper.Map<Proyectos>(proyectoDeserializado);

            if (usuariosExistentes.Count() > 1)
            {
                foreach(var usuario in proyectoUsuarioMappeado.Usuarios)
                {
                    usuario.Tipo = "Grupo";
                }
            }

            if (proyectosFotoMTFDto.ImgUrl is not null)
            {
                var urlImg = await almacenadorArchivos.Editar(proyectoUsuarioMappeado.ImgUrl, contenedor, proyectosFotoMTFDto.ImgUrl);
                proyectoUsuarioMappeado.ImgUrl = urlImg;
            }

            context.Add(proyectoUsuarioMappeado);
            await this.context.SaveChangesAsync();
            return Ok();


        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CreateProyectosDto proyectoConUsuariosDtos)
        {
            var proyectoActualizacion = await context.Proyectos.Include(pro => pro.Usuarios).FirstOrDefaultAsync(pro => pro.Id == id);

            if (proyectoActualizacion is null)
            {
                return NotFound();
            }

            proyectoActualizacion = mapper.Map(proyectoConUsuariosDtos, proyectoActualizacion);
            await context.SaveChangesAsync();
             
            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var proyecto = await context.Proyectos.Where(proy => proy.Id == id).ExecuteDeleteAsync();

            if (proyecto.Equals(0))
            {
                return NotFound();
            }

            return Ok();

        }


    }
}
