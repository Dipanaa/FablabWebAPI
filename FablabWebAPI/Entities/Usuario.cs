using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.Entities
{
    public class Usuario : IdentityUser<int>
    {

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Rut { get; set; }

        [EmailAddress]
        public string CorreoInstitucional { get; set; }

        public string Carrera { get; set; } 

        public int? LaboratorioId { get; set; }

        public int? RolId { get; set; } = 2; //TODO: Equivale a miembro pero se debe pasar a Roles de identity

        public Laboratorio? Laboratorio { get; set; }

        public Rol? Rol { get; set; } 

        public List<UsuarioProyecto> Proyectos { get; set; } = [];

    }
}
