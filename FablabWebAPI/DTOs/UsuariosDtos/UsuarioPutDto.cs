using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.DTOs.UsuariosDtos
{
    public class UsuarioPutDto
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Rut { get; set; }

        [EmailAddress]
        public string CorreoInstitucional { get; set; }

        public string Carrera { get; set; }

    }
}
