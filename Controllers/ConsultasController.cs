using Microsoft.AspNetCore.Mvc;
using CollegeKids2._0.Models;  // Asegúrate de que el modelo Consulta esté en este espacio de nombres

namespace CollegeKids2._0.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly CollegeKidsDbContext _context;  // Reemplaza 'YourDbContext' por 'CollegeKidsDbContext'

        // Inyección de dependencia para el DbContext
        public ConsultasController(CollegeKidsDbContext context)
        {
            _context = context;
        }

        // GET: ConsultasController
        public ActionResult Index()
        {
            return View();
        }

        // Acción POST para registrar una consulta
        [HttpPost]
        public JsonResult Registrar(string Nombre, string Correo, string Celular, string DetalleConsulta, string Seccion, string Grado)
        {
            try
            {
                // Crear un nuevo objeto de consulta
                var nuevaConsulta = new Consulta
                {
                    Nombre = Nombre,
                    Correo = Correo,
                    Celular = Celular,
                    DetalleConsulta = DetalleConsulta,
                    Seccion = Seccion,
                    Grado = Grado,
                    FechaConsulta = DateTime.Now  // Fecha actual
                };

                // Insertar la consulta en la base de datos
                _context.Consultas.Add(nuevaConsulta);
                _context.SaveChanges();

                // Retornar éxito
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
