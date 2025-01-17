using Microsoft.AspNetCore.Mvc;
using BibliotecaStandFree.Data;
using System.Threading.Tasks;

namespace BibliotecaStandFree.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionarOrdenesAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GestionarOrdenesAPI(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CambiarEstado")]
        public async Task<IActionResult> CambiarEstado([FromBody] string carritoId)
        {
            if (string.IsNullOrWhiteSpace(carritoId))
            {
                return BadRequest("El ID del carrito no puede estar vacío.");
            }

            var carrito = await _context.Carritos.FindAsync(carritoId);

            if (carrito == null)
            {
                return NotFound("Carrito no encontrado.");
            }

            carrito.CarStatus = "ENT"; // Cambiar el estado a 'ENT'
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Estado actualizado correctamente." });
        }
    }
}