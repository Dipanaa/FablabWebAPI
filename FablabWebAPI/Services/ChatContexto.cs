
using Azure.Core;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Packaging;
using Google.GenAI;
using Google.GenAI.Types;
using System.Text;
using System.Threading.Tasks;

namespace FablabWebAPI.Services
{
    public class ChatContexto : IChatContexto
    {
        private string _rutaDatos = string.Empty;
        private readonly IConfiguration configuration;

        public ChatContexto(IWebHostEnvironment webHostEnvironment,IConfiguration configuration)
        {
            _rutaDatos = Path.Combine(webHostEnvironment.ContentRootPath, "DatosLaboratorioChat");
            this.configuration = configuration;
        }


        public async Task<string> ObtenerContextoDeArchivosAsync()
        {
            var contextoDatos = new StringBuilder();

            if (!Directory.Exists(_rutaDatos))
            {
                return "Error al encontrar la ruta de datos";
            }

            var archivos = Directory.GetFiles(_rutaDatos);

            foreach (var archivo in archivos)
            {

                if (archivo.EndsWith(".docx"))
                {
                    contextoDatos.AppendLine(await LeerDocxAsync(archivo));
                }

                if (archivo.EndsWith(".csv"))
                {
                    contextoDatos.AppendLine(await LeerCsvAsync(archivo));

                }
            }

            return contextoDatos.ToString();

        }


        public async Task<string> LeerCsvAsync(string ruta)
        {
            return await File.ReadAllTextAsync(ruta);
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

        public async Task<string> ChatText(string? pregunta)
        {
            var contextoDatos = await ObtenerContextoDeArchivosAsync();
            var promptBase = $@"
                Eres un asistente experto en el laboratorio.
                Tu trabajo es responder las preguntas del usuario basándote ÚNICA Y EXCLUSIVAMENTE 
                en la siguiente información extraída de los archivos del laboratorio.
                No inventes información que no esté en este texto.
                Tambien tienes la capacidad de inferir la informacion de los csv en base a sus datos.
                Si la respuesta no se encuentra en el texto, di 'No encontré esa información en mis archivos'.
                Ademas puedes responder a saludos y necesito que del texto quites los *.

                --- CONTEXTO DE ARCHIVOS ---
                {contextoDatos}
                --- FIN DEL CONTEXTO ---

                PREGUNTA DEL USUARIO:
                {pregunta}
            ";

            var client = new Client(apiKey: this.configuration["GOOGLE_API_KEY"]);

            var response = await client.Models.GenerateContentAsync(
              model: "gemini-2.5-flash", contents: promptBase
            );

            return response.Candidates[0].Content.Parts[0].Text;

        }

    }
}
