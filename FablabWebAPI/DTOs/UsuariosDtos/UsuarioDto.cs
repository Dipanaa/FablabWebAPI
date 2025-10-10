using FablabWebAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.DTOs.UsuariosDtos
{
    public class UsuarioDto
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Rut { get; set; }

        [EmailAddress]
        public string CorreoInstitucional { get; set; }

        public string Carrera { get; set; }

        public string Telefono { get; set; }

        public int? LaboratorioId { get; set; }

        public Laboratorio? Laboratorio { get; set; }

        public int? RolId { get; set; } = 2;

        public Rol? Rol { get; set; }

        public List<Proyectos> Proyectos { get; set; } = [];

    }
}
