namespace FablabWebAPI.Entities
{
    public class Inventario
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Categoria { get; set; }
        public int Stock { get; set; }
        public int Ubicacion { get; set; }
        public int Descripcion { get; set; }
        public int Estado { get; set; }

        public int? LaboratorioId { get; set; }

        public Laboratorio? Laboratorio { get; set; }

    }
}
