using BibliotecaStandFree.Data;
using Microsoft.AspNetCore.Identity;
using BibliotecaStandFree.Models;
using Microsoft.EntityFrameworkCore;
using BibliotecaStandFree.Utils;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a MySQL
var connectionString = "server=localhost; port=3306; database=bibliotecastandfree; user=root; password=MySQLsa1Q%#12; Persist Security Info=False; Connect Timeout=300";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false; // Requiere al menos un dígito (esto ya es válido para 12345678.)
        options.Password.RequireUppercase = false; // No requiere mayúsculas
        options.Password.RequireLowercase = false; // No requiere minúsculas
        options.Password.RequireNonAlphanumeric = false; // Requiere al menos un carácter no alfanumérico
        options.Password.RequiredLength = 8; // Longitud mínima de 8
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Bloqueo por 5 minutos
        options.Lockout.MaxFailedAccessAttempts = 5; // Máximo 5 intentos fallidos
        options.SignIn.RequireConfirmedAccount = false; // No requiere confirmación de cuenta
    })
    .AddEntityFrameworkStores<ApplicationDbContext>() // Asegúrate de que ApplicationDbContext esté registrado
    .AddDefaultTokenProviders()
    .AddSignInManager<SignInManager<Usuario>>(); // Agrega explícitamente SignInManager para tu clase Usuario


builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Asignación del rol al usuario
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<Usuario>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await RoleHelper.AsignarRol(userManager, roleManager, "carlosconstantev@gmail.com", "Admin");
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();