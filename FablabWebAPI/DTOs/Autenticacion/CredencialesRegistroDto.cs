using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.DTOs.Autenticacion
{
    public class CredencialesRegistroDto
    {
        //TODO: Validar con @inacapmai.cl con el IValidator
        [EmailAddress]
        public string Email { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Rut {  get; set; }

        public string Carrera { get; set; }

        public string Telefono { get; set; }

        [Required]
        public required string Contrasena { get; set; }

    }
}
