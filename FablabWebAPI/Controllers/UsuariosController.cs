using AutoMapper;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs;
using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;

        public UsuariosController(ApplicationDbContext context, IMapper autoMapper) {

            this.context = context;
            this.mapper = autoMapper;

        }

        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get()
        {
            var listaUsuarios = await this.context.Usuario
                .Include(x => x.Laboratorio)
                .Include(x => x.Rol)
                .Include(x => x.Proyectos)
                .ToListAsync();
            return listaUsuarios;

        }

        [HttpPost]
        public async Task<ActionResult> Post(Usuario usuario)
        {
            context.Add(usuario);
            await this.context.SaveChangesAsync();
            return Ok();
        }

        //TODO: Agregar metodos put y delete


    }
}
