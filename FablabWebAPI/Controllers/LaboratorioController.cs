using FablabWebAPI.Datos;
using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/laboratorio")]
    public class LaboratorioController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LaboratorioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Laboratorio>> Get()
        {

            var listaLabs = await this.context.Laboratorio.Include((entity)=>entity.Noticias).ToListAsync();
            return listaLabs;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Laboratorio laboratorio)
        {
            context.Add(laboratorio);
            await this.context.SaveChangesAsync();  
            return Ok();


        }







    }
}
