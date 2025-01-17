using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using BibliotecaStandFree.Utils;

namespace BibliotecaStandFree.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Models.Usuario> _signInManager;
        private readonly UserManager<Models.Usuario> _userManager;

        public LoginModel(SignInManager<Models.Usuario> signInManager, UserManager<Models.Usuario> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
            [EmailAddress(ErrorMessage = "Por favor, introduce un correo válido.")]
            [Display(Name = "Correo Electrónico")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; } = string.Empty;
        }

        public int TotalItems { get; set; }

        public void OnGet()
        {
            // Calcular el total de ítems y el precio total del carrito
            TotalItems = CarritoHelper.ObtenerTotalItems(HttpContext.Session);

            // Pasar datos al ViewData
            ViewData["CartCount"] = TotalItems;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Buscar al usuario por correo electrónico
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    ErrorMessage = "El correo o la contraseña son incorrectos.";
                    return Page();
                }

                // Intentar iniciar sesión usando el UserName
                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName, 
                    Input.Password, 
                    isPersistent: false, 
                    lockoutOnFailure: false
                );

                if (result.Succeeded)
                {
                    user.LastLogin = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);
                    return RedirectToPage("/Index");
                }

                if (result.IsLockedOut)
                {
                    ErrorMessage = "La cuenta está bloqueada. Por favor, inténtalo más tarde.";
                }
                else
                {
                    ErrorMessage = "El correo o la contraseña son incorrectos.";
                }
            }
            catch (Exception ex)
            {
                // Manejar errores inesperados
                ErrorMessage = "Ocurrió un error al intentar iniciar sesión.";
                Console.WriteLine($"Error en Login: {ex.Message}");
            }

            return Page();
        }
    }
}
