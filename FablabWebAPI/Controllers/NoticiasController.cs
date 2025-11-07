using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.NoticiasDtos;
using FablabWebAPI.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/noticias")]
    public class NoticiasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        private readonly IValidator<Noticias> validator;

        public NoticiasController(ApplicationDbContext context, IMapper autoMapper, IValidator<Noticias> validator)
        {
            this.context = context;
            this.mapper = autoMapper;
            this.validator = validator;
        }


        [HttpGet]
        public async Task<IEnumerable<NoticiaDto>> Get() {

            var lista = await this.context.Noticias.ToListAsync();
            var listaNoticiaDto = mapper.Map<IEnumerable<NoticiaDto>>(lista);
            return listaNoticiaDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Noticias noticias)
        {
            var validatorNoticias = await this.validator.ValidateAsync(noticias);

            if (!validatorNoticias.IsValid)
            {
                return BadRequest();
            }


            context.Add(noticias);
            await this.context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entitiesEliminated = await this.context.Noticias.Where((entity) => entity.Id == id).ExecuteDeleteAsync();

            if (entitiesEliminated.Equals(0))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Noticias noticia)
        {
            var entityfind = await this.context.Noticias.AnyAsync((entity)=> entity.Id == id);

            if (!entityfind)
            {
                return BadRequest();
            }
            context.Update(noticia);
            await this.context.SaveChangesAsync();
            return Ok();
        }





    }
}
