using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.DTOs.UsuariosDtos
{
    public class UsuarioPerfilPutDto
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Rut { get; set; }

        public string Carrera { get; set; }

        public string Telefono { get; set; }

        public IFormFile? ImgUrl { get; set; }
    }
}
