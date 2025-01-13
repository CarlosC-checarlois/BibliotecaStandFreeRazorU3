using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Pages
{
    public class DetalleCartasModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetalleCartasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Carta Carta { get; set; } = new Carta();

        public async Task<IActionResult> OnGetAsync(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
            {
                return NotFound("El código de la carta es requerido.");
            }

            // Buscar la carta en la base de datos
            Carta = await _context.Cartas.FirstOrDefaultAsync(c => c.CarCodigo == codigo);

            if (Carta == null)
            {
                return NotFound("La carta no existe.");
            }

            return Page();
        }
    }
}