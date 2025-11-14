using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.GraficosDto;
using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/graficos")]
    public class GraficosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper autoMapper;
        private readonly ILogger<NotificacionesController> logger;

        public GraficosController(ApplicationDbContext context, IMapper autoMapper,
            ILogger<NotificacionesController> logger)
        {
            this.context = context;
            this.autoMapper = autoMapper;
            this.logger = logger;
        }

        [HttpGet("proyectoporusuario")]
        public async Task<ActionResult> GetProyectoPorUsuario()
        {
            //Labels de nombres de usuario del laboratorio
            var labelsNombre = new List<string>();
            var proyectosCuenta = new List<int>();
       

            //Tenemos todos los datos
            var proyectosPorUsuario = await context.Usuario.Select(user => new ProyectoPorUsuarioDatasetDto
            {
                Nombre = user.Nombre,
                Proyectos = user.Proyectos.Count()

            }).ToListAsync();

            labelsNombre = proyectosPorUsuario.Select(pro => pro.Nombre).ToList();
            proyectosCuenta = proyectosPorUsuario.Select(pro => pro.Proyectos).ToList();

            var dataDto = new ProyectoPorUsuarioDto
            {
                LabelsNombres = labelsNombre,
                ProyectosCuenta = proyectosCuenta,
            };

            return Ok(dataDto);

        }


    }
}
