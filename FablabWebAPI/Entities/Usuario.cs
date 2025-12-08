using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.Entities
{
    public class Usuario : IdentityUser<int>
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Rut { get; set; }

        [EmailAddress]
        public string CorreoInstitucional { get; set; }
        [Required]
        public string Carrera { get; set; }

        [Unicode(false)]
        public string? ImgUrl { get; set; }

        public int? LaboratorioId { get; set; }

        public int? RolId { get; set; } = 2; //TODO: Equivale a miembro pero se debe pasar a Roles de identity

        public Laboratorio? Laboratorio { get; set; }

        public Rol? Rol { get; set; } 

        public List<UsuarioProyecto> Proyectos { get; set; } = [];

    }
}
