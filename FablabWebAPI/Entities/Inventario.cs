using System.ComponentModel.DataAnnotations;

namespace FablabWebAPI.Entities
{
    public class Inventario
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Categoria { get; set; }
        [Required]
        public int Stock { get; set; }
      
        public string? Ubicacion { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public string Estado { get; set; }

        public int? LaboratorioId { get; set; }

        public Laboratorio? Laboratorio { get; set; }

    }
}
