using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.DTOs.NoticiasDtos
{
    public class NoticiaDto
    {
       
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Epigrafe { get; set; }
        public string Autor { get; set; }
        public DateTime? FechaPublicacion { get; set; } = DateTime.Now;
        public string Contenido { get; set; }
        public string? ImageUrlPrincipal { get; set; }

        public string? ImageUrlAutor { get; set; }

        public string Estado { get; set; } //Activo, Deshabilitado
    }
}
