using CollegeKids2._0.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace CollegeKids2._0.Controllers
{
    public class AuthController : Controller
    {
        private readonly CollegeKidsDbContext _context;

        public AuthController(CollegeKidsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            try
            {
                // Validar el usuario
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
                if (usuario == null || usuario.Contraseña != contrasena)
                {
                    return Json(new { success = false, message = "Credenciales inválidas." });
                }

                // Crear Claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Role, usuario.RolId.ToString())
        };

                var identity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal);

                // Redirigir según el rol
                if (usuario.RolId == 1 || usuario.RolId == 3)
                {
                    return Json(new { success = true, redirectUrl = "/Panel" });
                }

                return Json(new { success = true, redirectUrl = "/Home/Index" });
            }
            catch (Exception ex)
            {
                // Registrar el error (puedes cambiar esto para usar un logger en lugar de la consola)
                Console.WriteLine($"Error en el método Login: {ex.Message}");
                Console.WriteLine(ex.StackTrace);

                // Devolver un mensaje genérico de error
                return Json(new { success = false, message = "Ocurrió un error inesperado. Por favor, intente nuevamente más tarde." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
