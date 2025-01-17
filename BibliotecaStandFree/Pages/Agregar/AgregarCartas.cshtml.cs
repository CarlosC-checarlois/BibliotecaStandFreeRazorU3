using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using System.Threading.Tasks;

namespace BibliotecaStandFree.Pages.Gestionar
{
    public class AgregarCartasModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AgregarCartasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Carta Carta { get; set; }

        public void OnGet()
        {
            // Método para inicializar valores si es necesario
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Agrega la nueva carta a la base de datos
            _context.Cartas.Add(Carta);
            await _context.SaveChangesAsync();

            // Redirige a la página de gestión
            return RedirectToPage("/Gestionar/GestionarCartas");
        }
    }
}