using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace CollegeKids2._0.Controllers
{
    public class DocumentosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerarNoAdeudo(string nombreAlumno, string grado, string dniAlumno, string fecha)
        {
            // Ruta del PDF original en wwwroot
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfs/NO ADEUDO.pdf");
            string outputFileName = $"NO_ADEUDO_{fecha.Replace("/", "-")}.pdf";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/temp", outputFileName);

            // Asegúrate de que exista la carpeta temp
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/temp"));

            // Cargar el contenido del PDF
            byte[] pdfBytes = System.IO.File.ReadAllBytes(templatePath);

            // Reemplazar los placeholders
            string pdfText = System.Text.Encoding.UTF8.GetString(pdfBytes)
                .Replace("{{NombreAlumno}}", nombreAlumno)
                .Replace("{{Grado}}", grado)
                .Replace("{{DniAlumno}}", dniAlumno)
                .Replace("{{Fecha}}", fecha);

            // Guardar el nuevo PDF
            await System.IO.File.WriteAllTextAsync(outputPath, pdfText);

            // Devolver el archivo para descargar
            byte[] finalPdf = System.IO.File.ReadAllBytes(outputPath);
            return File(finalPdf, "application/pdf", outputFileName);
        }
    }
}
