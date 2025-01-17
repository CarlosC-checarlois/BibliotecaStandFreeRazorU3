using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Pages.Gestionar
{
    public class GestionarLibrosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GestionarLibrosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista de libros que será utilizada en la página
        public IList<Libro> Libros { get; set; }

        // Método para cargar la lista de libros
        public async Task<IActionResult> OnGetAsync()
        {
            // Obtener la lista de libros desde la base de datos
            Libros = await _context.Libros.ToListAsync();

            // Si no hay libros, retornar NotFound (opcional)
            if (Libros == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}