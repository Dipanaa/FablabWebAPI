using Microsoft.EntityFrameworkCore;

namespace FablabWebAPI.Entities
{
    [PrimaryKey(nameof(UsuarioId),nameof(ProyectosId))]
    public class UsuarioProyecto
    {
        public int UsuarioId { get; set; }
        public int ProyectosId { get; set; }

        public string? Tipo { get; set; } = "Individual";

        public Usuario? Usuario { get; set; }

        public Proyectos? Proyectos { get; set;}

    }
}
