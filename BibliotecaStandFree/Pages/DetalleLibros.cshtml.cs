using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Pages
{
    public class DetalleLibrosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetalleLibrosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Libro Libro { get; set; } = new Libro();

        public async Task<IActionResult> OnGetAsync(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
            {
                return NotFound("El código del libro es requerido.");
            }

            // Buscar el libro en la base de datos
            Libro = await _context.Libros.FirstOrDefaultAsync(l => l.LibCodigo == codigo);

            if (Libro == null)
            {
                return NotFound("El libro no existe.");
            }

            return Page();
        }
    }
}