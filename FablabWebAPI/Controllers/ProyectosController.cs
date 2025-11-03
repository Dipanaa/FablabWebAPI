using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.ProyectosDtos;
using FablabWebAPI.DTOs.UsuarioProyectoDtos;
using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/proyectos")]
    public class ProyectosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;

        public ProyectosController(ApplicationDbContext context, IMapper autoMapper)
        {
            this.context = context;
            this.mapper = autoMapper;

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
        public async Task<ActionResult> Post(CreateProyectosDto proyectosDto)
        {
            if(proyectosDto.Ids is null || proyectosDto.Ids.Count() == 0)
            {
                return BadRequest();
            }

            var usuariosExistentes = await context.Usuario.Where(user => proyectosDto.Ids.Contains(user.Id)).Select(user => user.Id).ToListAsync();

            if(usuariosExistentes.Count() != proyectosDto.Ids.Count())
            {
                //TODO:poner error de validacion 
                return NotFound();
            }

            var proyectoUsuarioMappeado = mapper.Map<Proyectos>(proyectosDto);
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
