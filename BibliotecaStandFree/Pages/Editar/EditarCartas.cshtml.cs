using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using System.Threading.Tasks;

namespace BibliotecaStandFree.Pages.Editar
{
    public class EditarCartasModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditarCartasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Carta Carta { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Error"] = "El ID proporcionado no es válido.";
                return RedirectToPage("/Gestionar/GestionarCartas");
            }

            // Intentamos obtener la carta por el ID
            Carta = await _context.Cartas.FindAsync(id);

            if (Carta == null)
            {
                TempData["Error"] = $"No se encontró ninguna carta con el ID {id}.";
                return RedirectToPage("/Gestionar/GestionarCartas");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validamos el modelo
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "El formulario contiene errores. Por favor, revisa los campos.";
                return RedirectToPage("/Gestionar/GestionarCartas");
            }

            // Intentamos encontrar la carta en la base de datos
            var cartaExistente = await _context.Cartas.FindAsync(Carta.CarCodigo);
            if (cartaExistente == null)
            {
                TempData["Error"] = "No se encontró la carta especificada.";
                return RedirectToPage("/Gestionar/GestionarCartas");
            }

            // Actualizamos los valores de la carta existente
            cartaExistente.CarNombre = Carta.CarNombre;
            cartaExistente.CarDescripcion = Carta.CarDescripcion;
            cartaExistente.CarPrecio = Carta.CarPrecio;
            cartaExistente.CarCantidad = Carta.CarCantidad;

            try
            {
                // Guardamos los cambios
                await _context.SaveChangesAsync();
                TempData["Success"] = "La carta se actualizó correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al actualizar la carta: {ex.Message}";
            }

            // Siempre redirigimos a GestionarCartas
            return RedirectToPage("/Gestionar/GestionarCartas");
        }
    }
}
