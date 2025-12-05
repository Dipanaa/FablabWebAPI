using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using FablabWebAPI.Datos;
using FablabWebAPI.DTOs.GraficosDto;
using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Globalization;
using System.Text.Json;

namespace FablabWebAPI.Controllers
{
    [ApiController]
    [Route("api/graficos")]
    public class GraficosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper autoMapper;
        private readonly ILogger<NotificacionesController> logger;
        private readonly string _rutaGraficoPPU = "DatosLaboratorioChat/proyectos_por_usuario.csv";


        public GraficosController(ApplicationDbContext context, IMapper autoMapper,
            ILogger<NotificacionesController> logger)
        {
            this.context = context;
            this.autoMapper = autoMapper;
            this.logger = logger;
        }

        //Dataset proyectos por usuario
        [HttpGet("proyectoporusuario")]
        public async Task<ActionResult> GetProyectoPorUsuario()
        {
            //Labels de nombres de usuario del laboratorio
            var labelsNombre = new List<string>();
            var proyectosCuenta = new List<int>();
       

            //Tenemos todos los datos
            var proyectosPorUsuario = await context.Usuario.Select(user => new ProyectoPorUsuarioDatasetDto
            {
                Nombre = user.Nombre,
                Proyectos = user.Proyectos.Count()

            }).ToListAsync();

            labelsNombre = proyectosPorUsuario.Select(pro => pro.Nombre).ToList();
            proyectosCuenta = proyectosPorUsuario.Select(pro => pro.Proyectos).ToList();

            var dataDto = new ProyectoPorUsuarioDto
            {
                LabelsNombres = labelsNombre,
                ProyectosCuenta = proyectosCuenta,
            };

            return Ok(dataDto);

        }


        //Dataset proyectos por fecha
        [HttpGet("proyectoporfecha")]
        public async Task<ActionResult> GetProyectoPorFecha()
        {

            //Tenemos todos los datos
            var proyectosPorUsuario = await context.Proyectos.GroupBy(pro => new {pro.FechaInicio!.Value.Month})
                                                             .Select(grupo => new ProyectoPorFechaDto
                                                             {
                                                                 Mes = grupo.Key.Month,
                                                                 Proyectos = grupo.Count()

                                                             })
                                                             .OrderBy(ppf => ppf.Mes)
                                                             .ToListAsync();

            return Ok(proyectosPorUsuario);

        }



        [HttpPost("actualizar-csv-ppu")]
        public async Task<IActionResult> ActualizarCsv()
        {
            //Conversion de datos
            var datosJsonRaw = await context.Proyectos.ToListAsync();
            var datosJson = autoMapper.Map<List<proyectosPorUsuarioCsvDto>>(datosJsonRaw);
            var datosJsonExpand = autoMapper.Map<List<ExpandoObject>>(datosJson);



            if (datosJsonExpand == null || datosJsonExpand.Count == 0)
                return BadRequest("El JSON no contiene datos.");

            // --- PASO 1: CREAR UNA LISTA NUEVA Y LIMPIA ---
            // En lugar de modificar la existente, creamos una nueva que garantizamos que sea plana.
            var datosLimpios = new List<dynamic>();

            foreach (IDictionary<string, object> filaOriginal in datosJsonExpand)
            {
                // Creamos un nuevo objeto dinámico para esta fila
                var filaLimpia = new ExpandoObject() as IDictionary<string, object>;

                foreach (var kvp in filaOriginal)
                {
                    var valor = kvp.Value;

                    if (valor is JsonElement element)
                    {
                        // EL SECRETO: Extraemos el valor real según su tipo
                        // Esto elimina el "JsonElement" que causa el error de IEnumerable
                        switch (element.ValueKind)
                        {
                            case JsonValueKind.String:
                                filaLimpia[kvp.Key] = element.GetString();
                                break;
                            case JsonValueKind.Number:
                                filaLimpia[kvp.Key] = element.GetRawText(); // Mantiene el formato numérico original
                                break;
                            case JsonValueKind.True:
                            case JsonValueKind.False:
                                filaLimpia[kvp.Key] = element.GetBoolean();
                                break;
                            case JsonValueKind.Null:
                                filaLimpia[kvp.Key] = "";
                                break;
                            default:
                                // Para cualquier otra cosa (objetos, arrays), lo forzamos a texto
                                filaLimpia[kvp.Key] = element.ToString();
                                break;
                        }
                    }
                    else
                    {
                        // Si ya era un dato normal (null, string, int), lo pasamos tal cual
                        filaLimpia[kvp.Key] = valor?.ToString() ?? "";
                    }
                }
                datosLimpios.Add(filaLimpia);
            }

            // --- PASO 2: CONFIGURACIÓN PARA SOBRESCRIBIR ---
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Como usamos FileMode.Create, SIEMPRE necesitamos cabeceras.
                // true = Escribir la fila de títulos (id, nombre, etc.)
                HasHeaderRecord = true
            };

            // --- PASO 3: ESCRIBIR ---
            using (var stream = System.IO.File.Open(_rutaGraficoPPU, FileMode.Create)) // Create borra y empieza de cero
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                // Escribimos la lista LIMPIA, no la original
                await csv.WriteRecordsAsync(datosLimpios);
            }

            return Ok(new { mensaje = "CSV actualizado correctamente.", registros = datosLimpios.Count });
        }




    }
}
