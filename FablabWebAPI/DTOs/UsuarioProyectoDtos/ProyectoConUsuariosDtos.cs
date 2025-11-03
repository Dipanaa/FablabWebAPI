using FablabWebAPI.DTOs.ProyectosDtos;
using FablabWebAPI.DTOs.UsuariosDtos;

namespace FablabWebAPI.DTOs.UsuarioProyectoDtos
{
    public class ProyectoConUsuariosDtos: ProyectosDto
    {
        public List<UsuarioDto> Usuarios { get; set; } = [];


    }
}
