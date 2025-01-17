using BibliotecaStandFree.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BibliotecaStandFree.Pages
{
    public class IndexModel : PageModel
    {
        public int TotalItems { get; set; }

        public void OnGet()
        {
            // Calcular el total de ítems y el precio total del carrito
            TotalItems = CarritoHelper.ObtenerTotalItems(HttpContext.Session);

            // Pasar datos al ViewData
            ViewData["CartCount"] = TotalItems;
        }
    }
}