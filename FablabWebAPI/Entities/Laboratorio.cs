namespace FablabWebAPI.Entities
{
    public class Laboratorio
    {
        public int Id { get; set; }
        public string NombreLaboratorio { get; set; }
        
        public int CantidadIntegrantes { get; set; }

        public List<Noticias> Noticias { get; set; } = new List<Noticias>();

        public List<FormulariosIngreso> Formularios { get; set; } = new List<FormulariosIngreso>();

    }
}
