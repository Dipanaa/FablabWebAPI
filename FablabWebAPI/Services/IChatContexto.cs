namespace FablabWebAPI.Services
{
    public interface IChatContexto
    {
       Task<string> ObtenerContextoDeArchivosAsync();
       Task<string> LeerDocxAsync(string ruta);
       Task<string> LeerCsvAsync(string ruta);
        Task<string> ChatText();


    }
}
