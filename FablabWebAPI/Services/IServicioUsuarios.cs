using FablabWebAPI.Entities;

namespace FablabWebAPI.Services
{
    public interface IServicioUsuarios
    {
        Task<Usuario?> ObtenerUsuario();

    }
}