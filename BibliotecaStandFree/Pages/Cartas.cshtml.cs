using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using BibliotecaStandFree.Utils;

namespace BibliotecaStandFree.Pages
{
    public class CartasModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Constructor para inyectar el contexto de la base de datos
        public CartasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Propiedades públicas para pasar datos a la vista
        public List<Carta> Cartas { get; set; } = new();
        public string? SearchQuery { get; set; }
        public string? SearchType { get; set; }
        public int TotalItems { get; set; }

        public async Task OnGetAsync(string? search, string? type)
        {
            // Calcular el total de ítems en el carrito
            TotalItems = CarritoHelper.ObtenerTotalItems(HttpContext.Session);
            ViewData["CartCount"] = TotalItems;

            // Obtener las cartas activas desde la base de datos
            var query = _context.Cartas.Where(c => c.CarStatus == "ACT").AsQueryable();

            // Aplicar filtros de búsqueda si están presentes
            if (!string.IsNullOrWhiteSpace(search))
            {
                SearchQuery = search;
                SearchType = type?.ToLower();

                query = SearchType switch
                {
                    "nombre" => query.Where(c => EF.Functions.Like(c.CarNombre, $"%{search}%")),
                    "precio" when decimal.TryParse(search, out var price) => query.Where(c => c.CarPrecio == price),
                    _ => query // Si el filtro es inválido, no aplicamos ningún cambio
                };
            }

            // Ejecutar la consulta y convertirla en una lista
            Cartas = await query.ToListAsync();
        }
    }
}