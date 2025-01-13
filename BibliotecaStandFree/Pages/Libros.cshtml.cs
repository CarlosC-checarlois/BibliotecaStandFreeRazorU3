using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;

namespace BibliotecaStandFree.Pages
{
    public class LibrosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Constructor para inyectar el contexto de la base de datos
        public LibrosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Propiedades públicas para pasar datos a la vista
        public List<Libro> Libros { get; set; } = new();
        public string? SearchQuery { get; set; }
        public string? SearchType { get; set; }

        public async Task OnGetAsync(string? search, string? type)
        {
            // Obtener los libros activos desde la base de datos
            var query = _context.Libros.Where(l => l.LibStatus == "ACT").AsQueryable();

            // Aplicar filtros de búsqueda si están presentes
            if (!string.IsNullOrWhiteSpace(search))
            {
                SearchQuery = search;
                SearchType = type?.ToLower();

                query = SearchType switch
                {
                    "nombre" => query.Where(l => l.LibNombre.Contains(search, StringComparison.OrdinalIgnoreCase)),
                    "precio" when decimal.TryParse(search, out var price) => query.Where(l => l.LibPrecio == price),
                    "categoria" => _context.LibrosXLibreriaCategorias
                        .Where(lxc => lxc.CategoriaId.ToString() == search)
                        .Select(lxc => lxc.Libro)
                        .Where(l => l.LibStatus == "ACT"),
                    _ => query // Si el filtro es inválido, no aplicamos ningún cambio.
                };
            }

            // Ejecutar la consulta y convertirla en una lista
            Libros = await query.ToListAsync();
        }
    }
}