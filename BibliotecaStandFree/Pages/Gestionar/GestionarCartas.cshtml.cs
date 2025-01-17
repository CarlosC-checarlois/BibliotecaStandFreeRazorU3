using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaStandFree.Pages.Gestionar
{
    public class GestionarCartasModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GestionarCartasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Carta> Cartas { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Obtener las cartas desde la base de datos
            Cartas = await _context.Cartas.ToListAsync();
            return Page();
        }
    }
}