using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FablabWebAPI.Datos
{
    public class ApplicationDbContext: IdentityDbContext<Usuario,IdentityRole<int>,int>
    {

        public ApplicationDbContext(DbContextOptions options) : base(options){
        }
        public DbSet<Noticias> Noticias { get; set; }
        
        public DbSet<Laboratorio> Laboratorio { get; set; }

        public DbSet<FormulariosIngreso> FormulariosIngreso { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Rol> Rol { get; set; }

        public DbSet<Proyectos> Proyectos { get; set; }

        public DbSet<UsuarioProyecto> UsuarioProyecto { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UsuarioProyecto>()
                .HasKey(up => new { up.UsuarioId, up.ProyectosId });

            builder.Entity<UsuarioProyecto>()
                .HasOne(up => up.Usuario)
                .WithMany(up => up.Proyectos)
                .HasForeignKey(up => up.UsuarioId);

            builder.Entity<UsuarioProyecto>()
                .HasOne(up => up.Proyectos)
                .WithMany(up => up.Usuarios)
                .HasForeignKey(up => up.ProyectosId);

            base.OnModelCreating(builder);
        }




    }
}
