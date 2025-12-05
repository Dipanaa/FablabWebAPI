using FablabWebAPI.Entities;

namespace FablabWebAPI.DTOs.GraficosDto
{
    public class proyectosPorUsuarioCsvDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string DescripcionProyecto { get; set; }

        public string AreaAplicacion { get; set; }

        public DateTime? FechaInicio { get; set; }

        public string? Estado { get; set; } = "En proceso";

    }
}
