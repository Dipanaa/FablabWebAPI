namespace FablabWebAPI.Entities
{
    public class Proyectos
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string DescripcionProyecto { get; set; }

        public string AreaAplicacion { get; set; }

        public DateTime? FechaInicio { get; set; }

        public string? ImgUrl { get; set; }

        public List<UsuarioProyecto> Usuarios { get; set; } = [];

        public List<HitoProyecto> HitoProyectos { get; set; } = [];


    }
}
