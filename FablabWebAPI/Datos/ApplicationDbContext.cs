using FablabWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FablabWebAPI.Datos
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options){
        }
        public DbSet<Noticias> Noticias { get; set; }
        
        public DbSet<Laboratorio> Laboratorio { get; set; }

        public DbSet<FormulariosIngreso> FormulariosIngreso { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Rol> Rol { get; set; }

        public DbSet<Proyectos> Proyectos { get; set; }


    }
}
