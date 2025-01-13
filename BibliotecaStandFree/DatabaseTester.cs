using BibliotecaStandFree.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BibliotecaStandFree
{
    public class DatabaseTester
    {
        public static void TestDatabase(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Prueba: Cuenta el número de registros en la tabla "Carritos"
                var carritosCount = context.Carritos.Count();
                Console.WriteLine($"Número de carritos: {carritosCount}");

                // Prueba: Consulta y muestra los datos de los carritos
                var carritos = context.Carritos.ToList();
                foreach (var carrito in carritos)
                {
                    Console.WriteLine($"Carrito: Código={carrito.CarCodigo}, Subtotal={carrito.CarSubtotal}, IVA={carrito.CarIva}, Total={carrito.CarTotal}, Estado={carrito.CarStatus}");
                }
            }
        }
    }
}