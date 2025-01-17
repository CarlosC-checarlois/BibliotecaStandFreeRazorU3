using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using System.Threading.Tasks;

namespace BibliotecaStandFree.Pages.Editar
{
    public class EditarLibrosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditarLibrosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Libro Libro { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["Error"] = "No se proporcionó un ID válido.";
                return RedirectToPage("/Gestionar/GestionarLibros");
            }

            Libro = await _context.Libros.FindAsync(id);

            if (Libro == null)
            {
                TempData["Error"] = $"No se encontró un libro con el ID {id}.";
                return RedirectToPage("/Gestionar/GestionarLibros");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validamos el modelo. Si no es válido, mostramos un mensaje de error y redirigimos.
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "El formulario contiene errores. Por favor revisa los campos.";
                return RedirectToPage("/Gestionar/GestionarLibros");
            }

            // Intentamos buscar el libro en la base de datos.
            var libroExistente = await _context.Libros.FindAsync(Libro.LibCodigo);
            if (libroExistente == null)
            {
                TempData["Error"] = "No se encontró el libro especificado.";
                return RedirectToPage("/Gestionar/GestionarLibros");
            }

            // Actualizamos los campos del libro.
            libroExistente.LibNombre = Libro.LibNombre;
            libroExistente.LibAutor = Libro.LibAutor;
            libroExistente.LibPrecio = Libro.LibPrecio;
            libroExistente.LibCantidad = Libro.LibCantidad;

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "El libro se ha actualizado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al guardar los cambios: {ex.Message}";
            }

            // Siempre redirigimos a GestionarLibros.
            return RedirectToPage("/Gestionar/GestionarLibros");
        }
    }
}
