using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.Autenticacion;
using FablabWebAPI.DTOs.NotificacionesDtos;
using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


//WARNING: En la version actual solo funciona con formularios de ingreso. La logica para expandir a otro tipo de notificaciones esta sujeto a futuras actualizaciones.

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/notificaciones")]
    public class NotificacionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Usuario> userManager;
        private readonly ILogger<NotificacionesController> logger;

        public NotificacionesController(ApplicationDbContext context, IMapper autoMapper,
            UserManager<Usuario> userManager, ILogger<NotificacionesController> logger)
        {
            this.context = context;
            this.mapper = autoMapper;
            this.userManager = userManager;
            this.logger = logger;
        }

        //Get de formularios de ingreso
        [HttpGet("ingreso")]
        public async Task<ActionResult<IEnumerable<FormularioIngresoDto>>> Get()
        {
            var formulariosIngreso = await context.FormulariosIngreso.ToListAsync();
            var notificacionesIngresoMappeado = mapper.Map<IEnumerable<NotificacionesRegistroDto>>(formulariosIngreso);

            return Ok(notificacionesIngresoMappeado);

        }

        //Post de formulario de ingreso
        [HttpPost("ingreso/{id:int}")]
        public async Task<ActionResult> Post(int id)
        {

            var formularioIngreso = await context.FormulariosIngreso.FirstOrDefaultAsync(fi => fi.Id == id);

            if(formularioIngreso is null)
            {
                return BadRequest();
            }

            var usuarioIngreso = new Usuario
            {
                UserName = formularioIngreso.Nombre.Replace(" ", ""), //Reemplazar espacios
                Nombre = formularioIngreso.Nombre, //de lab
                Email = formularioIngreso.Email,
                CorreoInstitucional = formularioIngreso.Email, //de lab
                Apellido = formularioIngreso.Apellido,
                Rut = formularioIngreso.Rut,
                Carrera = formularioIngreso.Carrera,
                PhoneNumber = formularioIngreso.Telefono.ToString(),

            };

            var usuarioCreado = await userManager.CreateAsync(usuarioIngreso, formularioIngreso.Contrasena);

            if (usuarioCreado.Succeeded)
            {
                await context.FormulariosIngreso.Where(fi => fi.Id == formularioIngreso.Id).ExecuteDeleteAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("ingreso/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var formularioDelete = await context.FormulariosIngreso.Where(fi => fi.Id == id).ExecuteDeleteAsync();

            if (formularioDelete.Equals(0))
            {
                return BadRequest();
            }

            return Ok();

        }






    }
}
