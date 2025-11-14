namespace FablabWebAPI.DTOs.InventariosController
{
    public class InventarioPutItemDto
    {
        public required string Nombre { get; set; }
        public required string Categoria { get; set; }
        public int Stock { get; set; }
        public string Ubicacion { get; set; }
    }
}
