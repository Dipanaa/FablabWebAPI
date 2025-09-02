using System.ComponentModel.DataAnnotations.Schema;

namespace FablabWebAPI.Entities
{
    public class FormulariosIngreso
    {

       public int Id { get; set; }
       public string CorreoInstitucional { get; set; }
       public string Nombre { get; set; }
       public string Contraseña { get; set; } 
       public string Rut {  get; set; }
       public string Carrera { get; set; }

       public int LaboratorioId { get; set; } 
       public Laboratorio Laboratorio {  get; set; }
       public int Telefono { get; set; }
       
   
    }
}
