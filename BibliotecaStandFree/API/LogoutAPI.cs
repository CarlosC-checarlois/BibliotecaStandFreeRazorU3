using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BibliotecaStandFree.Models;

namespace BibliotecaStandFree.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Solo usuarios autenticados pueden acceder
    public class LogoutAPI : ControllerBase
    {
        private readonly SignInManager<Usuario> _signInManager;

        public LogoutAPI(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Cierra la sesión
            return Ok(new { message = "Sesión cerrada correctamente." });
        }
    }
}