using CollegeKids2._0.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la cadena de conexi�n para el DbContext
builder.Services.AddDbContext<CollegeKidsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Habilitar autenticaci�n por cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.SameSite = SameSiteMode.Lax; // Adjust to None if cross-origin
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Enforce HTTPS
        options.LoginPath = "/Panel/Login";    // Nueva ruta para el inicio de sesi�n
        options.LogoutPath = "/Panel/Logout"; // Nueva ruta para el cierre de sesi�n
        options.AccessDeniedPath = "/Panel/AccessDenied"; // Ruta para denegaci�n de acceso
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

// Activar autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
