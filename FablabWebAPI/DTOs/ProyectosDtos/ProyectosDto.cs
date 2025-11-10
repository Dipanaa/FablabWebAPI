namespace FablabWebAPI.DTOs.ProyectosDtos
{
    public class ProyectosDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string DescripcionProyecto { get; set; }

        public string AreaAplicacion { get; set; }

        public string? ImgUrl { get; set; }

        public DateTime? FechaInicio { get; set; }
    }
}
