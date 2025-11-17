namespace FablabWebAPI.DTOs.InventariosController
{
    public class InventarioItemsDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Categoria { get; set; }
        public int Stock { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }
}
