namespace FablabWebAPI.DTOs.Autenticacion
{
    public class AutenticacionRespuestaDto
    {
        public required string token {  get; set; }
        public DateTime Expiracion { get; set; }

    }
}
