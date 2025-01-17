using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using BibliotecaStandFree.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BibliotecaStandFree.Pages.Usuario
{
    public class PanelModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PanelModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Propiedades del usuario
        public string UsuarioStatus { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido { get; set; }

        // Lista de carritos relacionados al usuario
        public List<CarritoViewModel> Carritos { get; set; }

        // Total de ítems en el carrito actual
        public int TotalItems { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Console.WriteLine("[OnGetAsync] Inicio de la ejecución del método OnGetAsync.");

            if (!User.Identity.IsAuthenticated)
            {
                Console.WriteLine("[OnGetAsync] Usuario no autenticado. Redirigiendo a la página de Login.");
                return RedirectToPage("/Login");
            }

            Console.WriteLine("[OnGetAsync] Usuario autenticado. Intentando obtener el ID del usuario.");

            // Usar el método FindFirstValue para obtener el ID del usuario autenticado
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(usuarioId))
            {
                Console.WriteLine("[OnGetAsync] No se pudo obtener el ID del usuario. Redirigiendo a la página de Login.");
                return RedirectToPage("/Login");
            }

            Console.WriteLine($"[OnGetAsync] ID del usuario obtenido: {usuarioId}");

            // Obtener información del usuario
            var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario == null)
            {
                Console.WriteLine("[OnGetAsync] No se encontró información del usuario en la base de datos. Redirigiendo a la página de Login.");
                return RedirectToPage("/Login");
            }

            Console.WriteLine($"[OnGetAsync] Información del usuario obtenida: {usuario.UsuNombre} {usuario.UsuApellido}");

            // Asignar valores del usuario
            UsuarioStatus = usuario.UsuStatus;
            UsuarioNombre = usuario.UsuNombre;
            UsuarioApellido = usuario.UsuApellido;

            try
            {
                Console.WriteLine("[OnGetAsync] Intentando obtener carritos relacionados al usuario.");

                // Obtener carritos relacionados al usuario
                Carritos = await _context.UsuarioXCarritos
                    .Where(uxc => uxc.UsuarioId == usuarioId)
                    .Include(uxc => uxc.Carrito) // Incluir la relación con Carrito
                    .OrderByDescending(uxc => uxc.UsuxcarFechaModificacion) // Ordenar por fecha de modificación
                    .Select(uxc => new CarritoViewModel
                    {
                        Codigo = uxc.Carrito.CarCodigo,
                        Fecha = uxc.UsuxcarFechaModificacion,
                        Estado = uxc.UsuxcarStatus
                    })
                    .ToListAsync();

                Console.WriteLine($"[OnGetAsync] Carritos obtenidos: {Carritos.Count} carritos encontrados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[OnGetAsync] Error al obtener carritos relacionados: {ex.Message}");
                throw;
            }

            try
            {
                Console.WriteLine("[OnGetAsync] Intentando calcular el total de ítems en el carrito actual.");

                // Calcular el total de ítems en el carrito actual
                TotalItems = CarritoHelper.ObtenerTotalItems(HttpContext.Session);
                Console.WriteLine($"[OnGetAsync] Total de ítems en el carrito: {TotalItems}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[OnGetAsync] Error al calcular el total de ítems en el carrito: {ex.Message}");
                throw;
            }

            // Pasar el conteo al ViewData
            ViewData["CartCount"] = TotalItems;
            Console.WriteLine("[OnGetAsync] Asignación de datos para la vista completada.");

            return Page();
        }
    }

    /// <summary>
    /// Clase para representar la vista del carrito.
    /// </summary>
    public class CarritoViewModel
    {
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
    }
}
