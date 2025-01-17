using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Models;
using Microsoft.AspNetCore.Identity;
using BibliotecaStandFree.Utils;

namespace BibliotecaStandFree.Pages.Admin
{
    public class PanelModel : PageModel
    {
        private readonly UserManager<Models.Usuario> _userManager;

        public PanelModel(UserManager<Models.Usuario> userManager)
        {
            _userManager = userManager;
        }

        public Models.Usuario Usuario { get; set; } = new Models.Usuario();
        public int TotalItems { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Obtener el ID del usuario actual
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                // Si el usuario no está autenticado, redirigir al login
                return RedirectToPage("/Login");
            }

            // Buscar el usuario por su ID
            Usuario = await _userManager.FindByIdAsync(userId);

            if (Usuario == null)
            {
                // Si el usuario no existe, redirigir al login
                return RedirectToPage("/Login");
            }

            // Calcular el total de ítems en el carrito
            TotalItems = CarritoHelper.ObtenerTotalItems(HttpContext.Session);

            // Pasar datos al ViewData para la vista
            ViewData["CartCount"] = TotalItems;

            return Page();
        }
    }
}