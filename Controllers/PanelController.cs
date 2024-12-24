using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeKids2._0.Controllers
{
    [Authorize] // Asegura que solo usuarios autenticados puedan acceder a este controlador
    public class PanelController : Controller
    {
        [Authorize(Roles = "1")] // Solo usuarios con RolId = 1 (Admin) pueden acceder
        public IActionResult Index()
        {
            // Aquí podrías cargar datos específicos para mostrar en el panel
            // Por ejemplo, lista de usuarios, estadísticas, etc.
            return View();
        }

        [Authorize(Roles = "1")] // Ejemplo de otra acción protegida
        public IActionResult Usuarios()
        {
            // Aquí podrías mostrar una lista de usuarios o alguna gestión relacionada.
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

    }
}
