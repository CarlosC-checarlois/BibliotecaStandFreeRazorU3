using BibliotecaStandFree.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaStandFree.Pages
{
    public class CarritoModel : PageModel
    {
        // Propiedad pública para pasar los datos del carrito a la vista
        public List<CarritoItem> Carrito { get; set; } = new();

        public IActionResult OnGet()
        {
            // Obtener el carrito desde la sesión
            Carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session).Values.ToList();

            return Page();
        }

        public IActionResult OnPostEliminar(string id, string tipo)
        {
            // Obtener el carrito desde la sesión
            var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);

            if (carrito.ContainsKey(id))
            {
                carrito.Remove(id); // Eliminar el producto del carrito
                CarritoHelper.GuardarCarrito(HttpContext.Session, carrito); // Actualizar la sesión
            }

            return RedirectToPage(); // Recargar la página
        }

        public IActionResult OnPostFinalizarCompra()
        {
            // Verificar si el usuario está autenticado
            if (!User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToPage("/Login");
            }

            // Obtener el carrito desde la sesión
            var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);

            if (!carrito.Any())
            {
                TempData["Error"] = "El carrito está vacío.";
                return RedirectToPage();
            }

            // Aquí iría la lógica para procesar la compra
            // Ejemplo: Guardar la compra en la base de datos y reducir el stock.

            // Vaciar el carrito después de finalizar la compra
            HttpContext.Session.Remove("Carrito");

            TempData["Success"] = "¡Compra realizada con éxito!";
            return RedirectToPage("/Gracias");
        }
    }
}