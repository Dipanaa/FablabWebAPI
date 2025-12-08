using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.InventariosController;
using FablabWebAPI.DTOs.UsuariosDtos;
using FablabWebAPI.Entities;
using FluentValidation;
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
        private readonly IValidator<Inventario> validator;

        public InventarioController(ApplicationDbContext context, IMapper autoMapper, IValidator<Inventario> validator)
        {
            this.context = context;
            this.mapper = autoMapper;
            this.validator = validator;
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

            var itemValidado = await validator.ValidateAsync(inventarioItemMappeado);

            if (!itemValidado.IsValid)
            {
                return BadRequest();
            }


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

            var itemValidado = await validator.ValidateAsync(itemEncontrado);

            if (!itemValidado.IsValid)
            {
                return BadRequest();
            }

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
