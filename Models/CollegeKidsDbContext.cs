using Microsoft.EntityFrameworkCore;

namespace CollegeKids2._0.Models
{
    public class CollegeKidsDbContext : DbContext  // El DbContext debe heredar de DbContext
    {
        public CollegeKidsDbContext(DbContextOptions<CollegeKidsDbContext> options) : base(options)
        {
        }

        // Define las tablas (DbSet) que se corresponden con tus entidades (modelos)
        public DbSet<Consulta> Consultas { get; set; }  // Aquí defines la tabla Consultas
        public DbSet<Usuarios> Usuarios { get; set; }  // Aquí defines la tabla Consultas
        public DbSet<Rol> Roles { get; set; } // Agregar esta línea

        // Puedes agregar más DbSet aquí si tienes otras tablas en la base de datos
        // public DbSet<Alumno> Alumnos { get; set; }
        // public DbSet<Padre> Padres { get; set; }
        // public DbSet<Profesora> Profesoras { get; set; }
        // public DbSet<Pagos> Pagos { get; set; }
    }
}
