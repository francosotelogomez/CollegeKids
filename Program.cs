using CollegeKids2._0.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión para el DbContext
builder.Services.AddDbContext<CollegeKidsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Habilitar autenticación por cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.SameSite = SameSiteMode.Lax; // Adjust to None if cross-origin
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Enforce HTTPS
        options.LoginPath = "/Panel/Login";    // Nueva ruta para el inicio de sesión
        options.LogoutPath = "/Panel/Logout"; // Nueva ruta para el cierre de sesión
        options.AccessDeniedPath = "/Panel/AccessDenied"; // Ruta para denegación de acceso
    });

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
