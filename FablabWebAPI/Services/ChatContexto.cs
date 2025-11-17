
using DocumentFormat.OpenXml.Packaging;
using System.Text;
using System.Threading.Tasks;
using Google.GenAI;
using Google.GenAI.Types;

namespace FablabWebAPI.Services
{
    public class ChatContexto : IChatContexto
    {
        private string _rutaDatos = string.Empty;

        public ChatContexto(IWebHostEnvironment webHostEnvironment)
        {
            _rutaDatos = Path.Combine(webHostEnvironment.ContentRootPath, "DatosLaboratorioChat");

        }


        public async Task<string> ObtenerContextoDeArchivosAsync()
        {
            var contextoDatos = new StringBuilder();

            if (!Directory.Exists(_rutaDatos))
            {
                return "Error al enncontrar la ruta de datos";
            }

            var archivos = Directory.GetFiles(_rutaDatos);

            foreach (var archivo in archivos)
            {

                if (archivo.EndsWith(".docx"))
                {
                    contextoDatos.AppendLine(await LeerDocxAsync(archivo));
                }

            }

            return contextoDatos.ToString();

        }


        public Task<string> LeerCsvAsync(string ruta)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LeerDocxAsync(string ruta)
        {
            try
            {
                // Usamos File.ReadAllBytes para leer el archivo que fue copiado al directorio de salida
                byte[] fileBytes = await File.ReadAllBytesAsync(ruta);
                using (var memoryStream = new MemoryStream(fileBytes))
                using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, false))
                {
                    return doc.MainDocumentPart.Document.Body.InnerText;
                }
            }
            catch (Exception ex)
            {
                return $"Error al leer {Path.GetFileName(ruta)}: {ex.Message}";
            }
        }

        public async Task<string> ChatText()
        {

            // The client gets the API key from the environment variable `GEMINI_API_KEY`.
            var client = new Client(apiKey: "AIzaSyCTS-CBYhTp6uUxPeijyVWpWy_C7-SP1BU");
            var response = await client.Models.GenerateContentAsync(
              model: "gemini-2.5-flash", contents: "Dime porque es bueno ser programador en 2025?"
            );

            return response.Candidates[0].Content.Parts[0].Text;


        }


    }
}
