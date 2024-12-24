using CollegeKids2._0.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;  // Necesario para trabajar con EF Core y métodos asincrónicos
using System.Linq;
using Microsoft.AspNetCore.Authorization.Infrastructure;  // Necesario para utilizar los métodos LINQ como FirstOrDefaultAsync

namespace CollegeKids2._0.Controllers
{
    public class LoginController : Controller
    {
        private readonly CollegeKidsDbContext _dbContext;

        public LoginController(CollegeKidsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(); // Devuelve la vista de inicio de sesión
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(string correo, string contrasena)
        {
            // Buscar al usuario por correo
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

            if (usuario == null || usuario.Contraseña != contrasena)
            {
                return Json(new { success = false, message = "Correo o contraseña incorrectos." });
            }

            // Crear los claims para el usuario autenticado
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuario.Nombre),
        new Claim(ClaimTypes.Email, usuario.Correo),
        new Claim(ClaimTypes.Role, usuario.RolId.ToString()) // Convertir RolId a string
    };

            var claimsIdentity = new ClaimsIdentity(claims, "login");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Iniciar sesión con cookies
            await HttpContext.SignInAsync("Cookies", claimsPrincipal);

            // Redirigir al usuario según su RolId sin ReturnUrl
            if (usuario.RolId == 1) // Si el usuario es admin
            {
                return Json(new { success = true, redirectUrl = "/Admin/Index" });
            }

            // Si el usuario no es admin, redirigir al Home
            return Json(new { success = true, redirectUrl = "/Home/Index" });
        }


        // GET: Login/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
