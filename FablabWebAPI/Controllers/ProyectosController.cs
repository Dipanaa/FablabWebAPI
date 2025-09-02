using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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


        [HttpGet]
        public async Task<IEnumerable<Proyectos>> Get()
        {
            var proyectos = await this.context.Proyectos.ToListAsync();
            return proyectos;
        }


        [HttpPost]
        public async Task<ActionResult> Post(Proyectos proyectos)
        {
            this.context.Add(proyectos);
            await this.context.SaveChangesAsync();
            return Ok();
        }



    }
}
