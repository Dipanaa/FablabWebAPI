
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace FablabWebAPI.Services
{
    public class AlmacenarArchivosDeImagenes : IAlmacenadorArchivos
    {
        private string? connectionString;

        public AlmacenarArchivosDeImagenes(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorageConnection");
        }


        public async Task<string> Agregar(string contenedor, IFormFile archivo)
        {

            //TODO: Reducir calidad de imagen

            
            //Conexion a el storage account de azure
            var cliente = new BlobContainerClient(this.connectionString,contenedor);
            await cliente.CreateIfNotExistsAsync();
            cliente.SetAccessPolicy(PublicAccessType.Blob);

            //Creamos extension de archivo para evitar nombres duplicados
            var extension = Path.GetExtension(archivo.Name);
            var nombreDeArchivo = $"{Guid.NewGuid()}{extension}";

            //Abrimos cliente blob de almacenamiento
            var blob = cliente.GetBlobClient(nombreDeArchivo);

            //Creamos encabezados para poder ver la imagen en el navegador
            var httpHeaders = new BlobHttpHeaders();
            httpHeaders.ContentType = archivo.ContentType;
            await blob.UploadAsync(archivo.OpenReadStream(), httpHeaders);
            return blob.Uri.ToString();




        }

        public async Task Borrar(string? url, string contenedor)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            var cliente = new BlobContainerClient(this.connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var nombreDeArchivo = Path.GetFileName(url);
            var blob = cliente.GetBlobClient(nombreDeArchivo);
            await blob.DeleteIfExistsAsync();

        }
    }
}
