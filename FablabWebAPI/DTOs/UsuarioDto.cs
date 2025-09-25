using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.DTOs
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

    }
}
