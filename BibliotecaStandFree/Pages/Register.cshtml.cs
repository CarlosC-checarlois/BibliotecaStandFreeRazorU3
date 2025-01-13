using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BibliotecaStandFree.Models;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaStandFree.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<Models.Usuario> _userManager;
        private readonly SignInManager<Models.Usuario> _signInManager;

        // Constructor para inyectar dependencias
        public RegisterModel(UserManager<Models.Usuario> userManager, SignInManager<Models.Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Propiedad pública para pasar datos a la vista
        [BindProperty]
        public InputModel Input { get; set; } = new();

        // Clase para manejar los datos del formulario
        public class InputModel
        {
            [Required(ErrorMessage = "El primer nombre es obligatorio.")]
            [Display(Name = "Primer Nombre")]
            public string UsuNombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "El primer apellido es obligatorio.")]
            [Display(Name = "Primer Apellido")]
            public string UsuApellido { get; set; } = string.Empty;

            [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
            [Display(Name = "Fecha de Nacimiento")]
            [DataType(DataType.Date)]
            public DateTime? UsuFechaNacimiento { get; set; }

            [Required(ErrorMessage = "El género es obligatorio.")]
            [Display(Name = "Género")]
            public string UsuGenero { get; set; } = string.Empty;

            [Required(ErrorMessage = "El teléfono es obligatorio.")]
            [Phone(ErrorMessage = "Formato de teléfono inválido.")]
            [Display(Name = "Teléfono")]
            public string UsuTelefono { get; set; } = string.Empty;

            [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
            [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
            [Display(Name = "Correo Electrónico")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "El apodo (usuario) es obligatorio.")]
            [Display(Name = "Usuario (Apodo)")]
            public string UsuApodo { get; set; } = string.Empty;

            [Display(Name = "¿Deseas recibir anuncios?")]
            public bool UsuPreferenciaAnuncios { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "Debes confirmar la contraseña.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
            [Display(Name = "Confirmar Contraseña")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            // Método ejecutado al cargar la página por primera vez
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Crear un nuevo usuario basado en los datos del formulario
            var newUser = new Models.Usuario
            {
                UsuNombre = Input.UsuNombre,
                UsuApellido = Input.UsuApellido,
                UsuFechaNacimiento = Input.UsuFechaNacimiento,
                UsuGenero = Input.UsuGenero,
                UsuTelefono = Input.UsuTelefono,
                Email = Input.Email,
                UserName = Input.UsuApodo,
                UsuApodo = Input.UsuApodo,
                UsuPreferenciaAnuncios = Input.UsuPreferenciaAnuncios,
                DateJoined = DateTime.UtcNow
            };

            // Intentar registrar al usuario
            var result = await _userManager.CreateAsync(newUser, Input.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return RedirectToPage("/Index");
            }

            // Registrar y mostrar los errores específicos
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
                Console.WriteLine($"Error: {error.Description}");
            }

            return Page();
        }

        
        
    }
}
