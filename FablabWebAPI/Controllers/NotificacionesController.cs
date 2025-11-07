using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.Autenticacion;
using FablabWebAPI.DTOs.NotificacionesDtos;
using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


//WARNING: En la version actual solo funciona con formularios de ingreso. La logica para expandir a otro tipo de notificaciones esta sujeto a futuras actualizaciones.

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("/api/notificaciones")]
    public class NotificacionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public NotificacionesController(ApplicationDbContext context, IMapper autoMapper)
        {
            this.context = context;
            this.mapper = autoMapper;

        }

        //Get de formularios de ingreso
        [HttpGet("registro")]
        public async Task<ActionResult<IEnumerable<FormularioIngresoDto>>> Get()
        {
            var formulariosIngreso = await context.FormulariosIngreso.ToListAsync();
            var notificacionesIngresoMappeado = mapper.Map<IEnumerable<NotificacionesRegistroDto>>(formulariosIngreso);

            return Ok(notificacionesIngresoMappeado);

        }

    }
}
