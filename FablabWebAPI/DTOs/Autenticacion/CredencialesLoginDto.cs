using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.DTOs.Autenticacion
{
    public class CredencialesLoginDto
    {
        [Required]
        public string Email { get; set; }

        public string Contrasena { get; set; }    

    }
}
