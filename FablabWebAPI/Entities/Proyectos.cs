namespace FablabWebAPI.Entities
{
    public class Proyectos
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string DescripcionProyecto { get; set; }

        public string? Metodologia { get; set; }
        public string AreaAplicacion { get; set; }

        public string? FechaInicio { get; set; }

        public int? UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

    }
}
