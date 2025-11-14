using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.InventariosController;
using FablabWebAPI.DTOs.UsuariosDtos;
using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/inventario")]
    public class InventarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;

        public InventarioController(ApplicationDbContext context, IMapper autoMapper)
        {
            this.context = context;
            this.mapper = autoMapper;

        }


        [HttpGet]
        public async Task<IEnumerable<InventarioItemsDto>> Get()
        {
            var inventarioItems = await this.context.Inventario.ToListAsync();
            var inventarioItemsMappeado = this.mapper.Map<IEnumerable<InventarioItemsDto>>(inventarioItems);

            return inventarioItemsMappeado;

        }

        [HttpPost]
        public async Task<ActionResult> Post(InventarioItemsDto inventarioItemsDto)
        {
            var inventarioItemMappeado = mapper.Map<Inventario>(inventarioItemsDto);
            context.Add(inventarioItemMappeado);
            await this.context.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(InventarioPutItemDto inventarioPutItemDto,int id)
        {
            var itemEncontrado = await context.Inventario.FirstOrDefaultAsync(x => x.Id == id);


            if (itemEncontrado is null)
            {
                return NotFound();
            }

            mapper.Map(inventarioPutItemDto, itemEncontrado);
            context.Update(itemEncontrado);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var itemEncontrado = await context.Inventario.Where(item => item.Id == id ).ExecuteDeleteAsync();

            if (itemEncontrado.Equals(0))
            {
                return NotFound();
            }


            return NoContent();

        }





    }
}
