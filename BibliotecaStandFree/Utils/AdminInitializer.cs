using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using BibliotecaStandFree.Models;

namespace BibliotecaStandFree.Utils
{
    public class AdminInitializer
    {
        public static async Task CreateAdminAsync(IServiceProvider serviceProvider)
        {
            // Obtén los servicios necesarios
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Datos del administrador
            var adminEmail = "admin@biblioteca.com";
            var adminUserName = "Admin";
            var adminPassword = "Admin123!"; // Cambia esta contraseña
            var adminRole = "Admin";

            // Verifica si el rol Admin ya existe
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(adminRole));
                if (!roleResult.Succeeded)
                {
                    Console.WriteLine("Error al crear el rol Admin:");
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine($" - {error.Description}");
                    }
                    return;
                }
            }

            // Verifica si el usuario administrador ya existe
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                // Crea el usuario administrador
                var adminUser = new Usuario
                {
                    UsuNombre = "Administrador",
                    UsuApellido = "Sistema",
                    Email = adminEmail,
                    UserName = adminUserName,
                    UsuApodo = adminUserName,
                    UsuFechaNacimiento = new DateTime(2000, 1, 1),
                    UsuGenero = "M",
                    UsuTelefono = "0999999999",
                    UsuPreferenciaAnuncios = true,
                    UsuStatus = "Activo",
                    DateJoined = DateTime.UtcNow,
                };

                var userResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (!userResult.Succeeded)
                {
                    Console.WriteLine("Error al crear el usuario administrador:");
                    foreach (var error in userResult.Errors)
                    {
                        Console.WriteLine($" - {error.Description}");
                    }
                    return;
                }
                Console.WriteLine("Usuario administrador creado exitosamente.");
            }
            else
            {
                Console.WriteLine("El usuario administrador ya existe.");
            }

            // Asignar el rol de administrador
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (!await userManager.IsInRoleAsync(admin, adminRole))
            {
                var roleResult = await userManager.AddToRoleAsync(admin, adminRole);
                if (roleResult.Succeeded)
                {
                    Console.WriteLine("Rol 'Admin' asignado al usuario administrador.");
                }
                else
                {
                    Console.WriteLine("Error al asignar el rol Admin:");
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine($" - {error.Description}");
                    }
                }
            }
        }
    }
}
