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
            var proyectos = await context.Proyectos
                .Include(pro=> pro.Usuarios)
                    .ThenInclude(up => up.Usuario)
                .Include(pro=> pro.HitoProyectos)
                .ToListAsync();

            var proyectosDto = mapper.Map<IEnumerable<ProyectoConUsuariosDtos>>(proyectos);
            return proyectosDto;
        }


        //Get de proyecto en base a un id de usuario
        [HttpGet("{id:int}")]
        public async Task<IEnumerable<ProyectosDto>> Get(int id) 
        {
            var proyectosDeUsuario = await context.UsuarioProyecto
                .Include(up => up.Proyectos)
                .ThenInclude(pro => pro.HitoProyectos)
                .Where(up => up.UsuarioId == id)
                .Select(up => up.Proyectos)
                .ToListAsync();
                

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
        public async Task<ActionResult> Put(int id, PutProyectosDto putProyectosDto)
        {
            var proyectoActualizacion = await context.Proyectos
                .FirstOrDefaultAsync(pro => pro.Id == id);

            if (proyectoActualizacion is null)
            {
                return NotFound();
            }

            proyectoActualizacion = mapper.Map(putProyectosDto, proyectoActualizacion);
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

        //Hitos de proyecto

        //Recibimos el id del proyecto
        [HttpPost("hitoproyecto/{id:int}")]
        public async Task<ActionResult> PostHito(int id, PutHitoProyectoDto hitoProyectoDto)
        {
            var proyectoEncontrado = await context.Proyectos.FirstOrDefaultAsync(pro => pro.Id == id);

            if(proyectoEncontrado is null)
            {

                return NotFound();

            }

            var hitoProyecto = mapper.Map<HitoProyecto>(hitoProyectoDto);

            hitoProyecto.ProyectosId = id;

            context.Add(hitoProyecto);
            await context.SaveChangesAsync();

            return Ok();
            
        }


        //Recibimos el id del hito
        [HttpPut("hitoproyecto/hito/{id:int}")]
        public async Task<ActionResult> PutHito(int id, PutHitoProyectoDto putHitoProyectoDto)
        {
            var hitoProyecto = await context.HitoProyecto.FirstOrDefaultAsync(hp => hp.Id == id);

            if(hitoProyecto is null)
            {
                return NotFound();
            }

            mapper.Map(putHitoProyectoDto, hitoProyecto);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("hitoproyecto/hito/{id:int}")]
        public async Task<ActionResult> DeleteHito(int id)
        {
            var hitoProyecto = await context.HitoProyecto.Where(hp => hp.Id == id).ExecuteDeleteAsync();

            if (hitoProyecto.Equals(0))
            {
                return NotFound();
            }

            return Ok();


        }







    }
}
