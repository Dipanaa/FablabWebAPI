namespace FablabWebAPI.DTOs.NotificacionesDtos
{
    public class NotificacionesRegistroDto
    {
        public string Email { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Rut { get; set; }

        public string Carrera { get; set; }

        public DateTime Fecha { get; set; }  

        public string Tipo { get; set; } = "Registro";

    }
}
