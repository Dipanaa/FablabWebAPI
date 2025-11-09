namespace FablabWebAPI.Services
{
    //Interfaz con metodos para guardar archivos, que inteligente la interfaz :)
    public interface IAlmacenadorArchivos
    {
        Task Borrar(string? url, string contenedor);

        Task<string> Agregar(string contenedor, IFormFile archivo);

        async Task<string> Editar(string? urlAnterior, string contenedor, IFormFile archivo)
        {
            await Borrar(urlAnterior, contenedor);

            return await Agregar(contenedor, archivo);
        }
        

    }
}
