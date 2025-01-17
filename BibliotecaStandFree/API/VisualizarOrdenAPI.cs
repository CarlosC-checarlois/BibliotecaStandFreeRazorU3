using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaStandFree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisualizarOrdenAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VisualizarOrdenAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener los detalles de una orden específica.
        /// Incluye información de libros, productos de carta, y el usuario asociado.
        /// </summary>
        /// <param name="id">Código de la orden</param>
        /// <returns>Detalles de la orden</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdenDetalle(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { mensaje = "El ID de la orden es requerido." });
            }

            // Obtener la orden con sus relaciones
            var orden = await _context.Carritos
                .Include(c => c.LibrosXCarritos) // Incluir libros en la orden
                    .ThenInclude(lxc => lxc.Libro)
                .Include(c => c.CartaXCarritos) // Incluir productos de carta en la orden
                    .ThenInclude(cxc => cxc.Carta)
                .FirstOrDefaultAsync(c => c.CarCodigo == id);

            if (orden == null)
            {
                return NotFound(new { mensaje = "Orden no encontrada." });
            }

            // Obtener el usuario asociado a la orden
            var usuarioXCarrito = await _context.UsuarioXCarritos
                .Include(uxc => uxc.Usuario)
                .FirstOrDefaultAsync(uxc => uxc.CarritoId == orden.CarCodigo);

            var usuario = usuarioXCarrito?.Usuario;

            // Construir el objeto de respuesta
            var respuesta = new
            {
                orden.CarCodigo,
                orden.CarSubtotal,
                orden.CarIva,
                orden.CarTotal,
                orden.CarStatus,
                Usuario = usuario == null ? null : new
                {
                    usuario.Id,
                    usuario.UsuNombre,
                    usuario.UsuApellido,
                    usuario.Email
                },
                Libros = orden.LibrosXCarritos.Select(libro => new
                {
                    libro.LibCodigo,
                    libro.Libro.LibNombre,
                    libro.LibxcarCantidad,
                    libro.LibxcarTotal
                }),
                Cartas = orden.CartaXCarritos.Select(carta => new
                {
                    carta.CartaCodigo,
                    carta.Carta.CarNombre,
                    carta.CarxcarCantidad,
                    carta.CarxcarTotal
                })
            };

            return Ok(respuesta);
        }
    }
}
