using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeKids2._0.Models
{
    public class Usuarios
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; }

        [Required]
        [StringLength(255)]  // Asegúrate de que sea lo suficientemente largo para contraseñas hasheadas
        public string Contraseña { get; set; }

        public int RolId { get; set; }  // Ejemplo: Admin, User

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [ForeignKey("RolId")]
        public Rol Rol { get; set; } // Relación de navegación hacia el modelo Rol
    }
}
