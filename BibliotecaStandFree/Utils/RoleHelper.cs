using Microsoft.AspNetCore.Identity;
using BibliotecaStandFree.Models;
using System;
using System.Threading.Tasks;

namespace BibliotecaStandFree.Utils
{
    public static class RoleHelper
    {
        public static async Task AsignarRol(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, string email, string rol)
        {
            var usuario = await userManager.FindByEmailAsync(email);
            if (usuario == null)
            {
                Console.WriteLine($"Usuario con correo {email} no encontrado.");
                return;
            }

            // Crear el rol si no existe
            if (!await roleManager.RoleExistsAsync(rol))
            {
                var resultadoRol = await roleManager.CreateAsync(new IdentityRole(rol));
                if (!resultadoRol.Succeeded)
                {
                    Console.WriteLine($"Error al crear el rol {rol}:");
                    foreach (var error in resultadoRol.Errors)
                    {
                        Console.WriteLine($" - {error.Description}");
                    }
                    return;
                }
            }

            // Asignar el rol al usuario
            if (!await userManager.IsInRoleAsync(usuario, rol))
            {
                var resultadoAsignarRol = await userManager.AddToRoleAsync(usuario, rol);
                if (resultadoAsignarRol.Succeeded)
                {
                    Console.WriteLine($"Rol '{rol}' asignado al usuario {usuario.UserName}.");
                }
                else
                {
                    Console.WriteLine($"Error al asignar el rol {rol}:");
                    foreach (var error in resultadoAsignarRol.Errors)
                    {
                        Console.WriteLine($" - {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"El usuario {usuario.UserName} ya tiene el rol '{rol}'.");
            }
        }
    }
}
