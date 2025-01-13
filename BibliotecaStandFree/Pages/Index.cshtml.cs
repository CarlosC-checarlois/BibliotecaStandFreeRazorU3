using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Models;

namespace BibliotecaStandFree.Pages
{
    public class IndexModel : PageModel
    {
        public int TotalItems { get; set; }

        public void OnGet()
        {
            // Lógica para obtener datos, como el total de ítems en el carrito
            TotalItems = 10; // Ejemplo de dato dinámico
        }
    }
}
