using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Models;
using BibliotecaStandFree.Data;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaStandFree.Pages.Gestionar
{
    public class GestionarOrdenesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GestionarOrdenesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Carrito> Ordenes { get; set; }

        public void OnGet()
        {
            // Obtener órdenes con estado ACT o FIN, ordenadas por código descendente
            Ordenes = _context.Carritos
                .Where(c => c.CarStatus == "ACT" || c.CarStatus == "FIN")
                .OrderByDescending(c => c.CarCodigo)
                .ToList();
        }
    }
}