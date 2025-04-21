using CollegeKids2._0.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;
using iText.Layout;
namespace CollegeKids2._0.Controllers
{
    [Authorize] // Asegura que solo usuarios autenticados puedan acceder a este controlador
    public class PanelController : Controller
    {
        private readonly CollegeKidsDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PanelController(CollegeKidsDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        [Authorize(Roles = "1,3")] // Solo usuarios con RolId = 1 (Admin) pueden acceder
        public IActionResult Index()
        {
            // Aquí podrías cargar datos específicos para mostrar en el panel
            // Por ejemplo, lista de usuarios, estadísticas, etc.
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Consultas()
        {
            var consultas = _context.Consultas.ToList();
            return View(consultas);
        }
        // Acción para mostrar el detalle de una consulta
        public IActionResult ConsultasDetalles(int id)
        {
            var consulta = _context.Consultas.FirstOrDefault(c => c.Id == id);
            if (consulta == null)
            {
                return NotFound();
            }
            return View(consulta);
        }

        [HttpPost]
        public IActionResult UpdateConsulta([FromBody] ConsultaUpdateModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Datos no válidos." });
            }

            int id = model.Id;
            bool atendida = model.Atendida;

            // Aquí va tu lógica para actualizar la consulta
            // Ejemplo:
            var consulta = _context.Consultas.FirstOrDefault(c => c.Id == id);
            if (consulta == null)
            {
                return Json(new { success = false, message = "Consulta no encontrada." });
            }

            consulta.Atendida = atendida;
            _context.SaveChanges();

            return Json(new { success = true, message = "Consulta actualizada correctamente." });
        }

        public class ConsultaUpdateModel
        {
            public int Id { get; set; }
            public bool Atendida { get; set; }
        }
        [Authorize(Roles = "1")]
        public IActionResult Usuarios()
        {
            var usuarios = _context.Usuarios.Include(u => u.Rol).ToList();
            return View(usuarios);
        }

        // Crear un nuevo usuario (GET para mostrar el formulario)
        [HttpGet]
        public IActionResult CrearUsuario()
        {
            ViewBag.Roles = _context.Roles.ToList(); // Carga todos los roles desde la base de datos

            return View();
        }

        // Crear un nuevo usuario (POST para guardar los datos)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearUsuario(Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Use a logger in production
                }
            }


            usuario.FechaRegistro = DateTime.Now;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Usuarios");


        }

        // Editar un usuario existente (GET para mostrar el formulario)
        [HttpGet]
        public IActionResult EditarUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewBag.Roles = _context.Roles.ToList(); // Carga todos los roles desde la base de datos

            return View(usuario);
        }

        // Editar un usuario existente (POST para guardar los cambios)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarUsuario(Usuarios usuario)
        {

            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            return RedirectToAction("Usuarios");


        }
        [Authorize(Roles = "1")]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.Include(u => u.Rol).FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Eliminar un usuario
        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult EliminarUsuario(Usuarios model)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == model.Id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return RedirectToAction("Usuarios");
        }
        public IActionResult Documentos()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GenerarNoAdeudo(string nombreAlumno, string grado, string dniAlumno, string fecha)
        {
            // Ruta del archivo PDF original
            var archivoOriginal = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs", "NO_ADEUDO.pdf");

            // Creamos un nuevo archivo en memoria para escribir el PDF modificado
            var ms = new MemoryStream();

            try
            {
                // Abrir el PDF original para lectura
                using (var pdfReader = new PdfReader(archivoOriginal))
                {
                    // Crear un escritor de PDF en el MemoryStream
                    using (var pdfWriter = new PdfWriter(ms))
                    {
                        // Abrir el documento
                        var pdfDoc = new PdfDocument(pdfReader, pdfWriter);
                        var document = new Document(pdfDoc);

                        // Aquí vas a buscar y reemplazar los textos, ejemplo usando iTextSharp
                        var numPages = pdfDoc.GetNumberOfPages();
                        for (int pageNum = 1; pageNum <= numPages; pageNum++)
                        {
                            var page = pdfDoc.GetPage(pageNum);
                            var content = page.GetContentBytes();

                            // Reemplazar el texto en el PDF
                            string textoOriginal = Encoding.UTF8.GetString(content);
                            textoOriginal = textoOriginal.Replace("{{NombreAlumno}}", nombreAlumno)
                                                          .Replace("{{Grado}}", grado)
                                                          .Replace("{{DniAlumno}}", dniAlumno)
                                                          .Replace("{{Año}}", DateTime.Now.Year.ToString())  // Reemplaza con el año actual
                                                          .Replace("{{Fecha}}", fecha);

                            // Escribir el contenido modificado de vuelta en la página (este paso depende de tu implementación de manipulación de texto en el PDF)
                            // Esto es solo un ejemplo básico, probablemente necesitarás más lógica para reemplazar los campos correctamente.
                        }
                    }
                }

                // Antes de devolver el archivo, restablecemos la posición del MemoryStream al inicio
                ms.Position = 0;

                // Retornar el archivo PDF modificado para su descarga
                return File(ms.ToArray(), "application/pdf", "NO_ADEUDO_MODIFICADO.pdf");
            }
            catch (Exception ex)
            {
                // Manejar cualquier error
                return BadRequest($"Error al generar el PDF: {ex.Message}");
            }
            finally
            {
                // Asegurarse de que el MemoryStream se cierra solo cuando terminamos
                ms.Close();
            }
        }
    }
}
