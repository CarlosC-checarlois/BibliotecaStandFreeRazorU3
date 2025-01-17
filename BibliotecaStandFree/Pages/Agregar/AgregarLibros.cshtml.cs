using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using System.Threading.Tasks;

namespace BibliotecaStandFree.Pages.Agregar
{
    public class AgregarLibrosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AgregarLibrosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Libro Libro { get; set; }

        public void OnGet()
        {
            // Solo muestra el formulario en la página
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Agregar el nuevo libro a la base de datos
            _context.Libros.Add(Libro);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Gestionar/GestionarLibros");
        }
    }
}