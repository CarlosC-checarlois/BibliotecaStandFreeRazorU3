using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BibliotecaStandFree.Models;
using BibliotecaStandFree.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaStandFree.Pages.Visualizar
{
    public class VisualizarOrdenModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public VisualizarOrdenModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Propiedades para manejar la orden y sus relaciones
        public Carrito Orden { get; set; } = new();
        public List<LibrosXCarrito> Libros { get; set; } = new();
        public List<CartaXCarrito> Cartas { get; set; } = new();
        public Models.Usuario UsuarioOrden { get; set; } = new();

        /// <summary>
        /// Método para manejar la obtención de datos de una orden específica.
        /// </summary>
        /// <param name="id">El código de la orden.</param>
        /// <returns>La página de visualización de la orden o redirección si no se encuentra.</returns>
        public async Task<IActionResult> OnGetAsync(string id)
        {
            // Validar que el ID no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToPage("/Gestionar/GestionarOrdenes");
            }

            // Obtener la orden con relaciones necesarias
            Orden = await _context.Carritos
                .Include(c => c.LibrosXCarritos)
                    .ThenInclude(lxc => lxc.Libro)
                .Include(c => c.CartaXCarritos)
                    .ThenInclude(cxc => cxc.Carta)
                .FirstOrDefaultAsync(c => c.CarCodigo == id);

            if (Orden == null)
            {
                return RedirectToPage("/Gestionar/GestionarOrdenes");
            }

            // Cargar libros asociados
            Libros = await _context.LibrosXCarrito
                .Include(lxc => lxc.Libro) // Relación con Libros
                .Where(lxc => lxc.CarCodigo == id)
                .ToListAsync();

            // Cargar productos de carta asociados
            Cartas = await _context.CartaXCarrito
                .Include(cxc => cxc.Carta) // Relación con Cartas
                .Where(cxc => cxc.CarCodigo == id)
                .ToListAsync();

            // Obtener el usuario asociado a la orden
            var usuarioCarrito = await _context.UsuarioXCarritos
                .Include(uxc => uxc.Usuario)
                .FirstOrDefaultAsync(uxc => uxc.CarritoId == id);

            UsuarioOrden = usuarioCarrito?.Usuario;

            return Page();
        }
    }
}
