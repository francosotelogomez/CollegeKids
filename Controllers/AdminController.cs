using CollegeKids2._0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeKids2._0.Controllers
{
    [Authorize(Roles = "Admin")]  // Solo los usuarios con el rol 'Admin' pueden acceder

    public class AdminController : Controller

    {
        private readonly CollegeKidsDbContext _context;  // Reemplaza 'YourDbContext' por 'CollegeKidsDbContext'
        public AdminController(CollegeKidsDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var consultas = _context.Consultas.ToList();  // Obtener todas las consultas
            return View(consultas);  // Pasar la lista de consultas al modelo de la vista
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]  // Solo el admin puede realizar esta acción
        public IActionResult MarcarComoAtendida(int consultaId)
        {
            var consulta = _context.Consultas.FirstOrDefault(c => c.Id == consultaId);
            if (consulta != null)
            {
                consulta.Atendida = true;
                _context.SaveChanges();  // Guarda los cambios en la base de datos
            }

            return RedirectToAction("Index");
        }

    }
}
