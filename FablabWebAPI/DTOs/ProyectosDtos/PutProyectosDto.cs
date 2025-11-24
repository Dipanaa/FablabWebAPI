namespace FablabWebAPI.DTOs.ProyectosDtos
{
    public class PutProyectosDto
    {
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string DescripcionProyecto { get; set; }

        public string AreaAplicacion { get; set; }

        public DateTime? FechaInicio { get; set; }

    }
}
