using System.ComponentModel.DataAnnotations.Schema;

namespace FablabWebAPI.Entities
{ 
    //TODO. Agregar Estado
    public class FormulariosIngreso
    {

       public int Id { get; set; }
       public string Email { get; set; }
       public string Nombre { get; set; }
       public string Apellido { get; set; }    
       public string Contrasena { get; set; } 
       public string Rut {  get; set; }
       public string Carrera { get; set; }

       public DateTime Fecha { get; set; } = DateTime.Now;
       public int LaboratorioId { get; set; } = 1;
       public Laboratorio Laboratorio {  get; set; }
       public int Telefono { get; set; }
       
   
    }
}
