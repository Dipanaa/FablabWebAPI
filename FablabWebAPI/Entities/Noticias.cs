using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FablabWebAPI.Entities
{
    public class Noticias
    {
        public int Id {  get; set; }

        [Required, MinLength(10)]
        public string Titulo { get; set; }

        [Required,MinLength(10)]
        public string Epigrafe { get; set; }

        [Required, MinLength(10)]
        public string Autor { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Contenido { get; set; }
        public string? ImageUrlPrincipal  { get; set; }

        public string? ImageUrlAutor { get; set; }

        public string Estado { get; set; } = "Activo";

        public int? LaboratorioId { get; set; }

        public Laboratorio? Laboratorio { get; set; }


    }
}
