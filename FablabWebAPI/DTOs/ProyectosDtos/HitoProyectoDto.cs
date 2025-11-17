namespace FablabWebAPI.DTOs.ProyectosDtos
{
    public class HitoProyectoDto
    {
        public int id { get; set; }
        public required string NombreHito { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; } = DateTime.Now;
    }
}
