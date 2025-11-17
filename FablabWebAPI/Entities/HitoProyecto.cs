namespace FablabWebAPI.Entities
{
    public class HitoProyecto
    {
        public int Id { get; set; }
        public required string NombreHito { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public int? ProyectosId { get; set; }

        public Proyectos? Proyectos { get; set; }
    }
}
