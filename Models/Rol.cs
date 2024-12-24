namespace CollegeKids2._0.Models
{
    public class Rol
    {
        public int Id { get; set; } // Identificador único para el rol
        public string Nombre { get; set; } // Nombre del rol, como "Admin", "Usuario", etc.

        // Relación con los usuarios
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
