using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using BibliotecaStandFree.Utils;
using System;
using Microsoft.Extensions.Logging;

using System.Linq;
using System.Threading.Tasks;
public class FinalizarCompraAPI : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<FinalizarCompraAPI> _logger;

    public FinalizarCompraAPI(ApplicationDbContext context, ILogger<FinalizarCompraAPI> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("finalizar")]
    public async Task<IActionResult> FinalizarCompra()
    {
        _logger.LogInformation("[FinalizarCompra] Inicio del proceso de finalización de compra.");

        if (!User.Identity.IsAuthenticated)
        {
            _logger.LogWarning("[FinalizarCompra] Usuario no autenticado.");
            return Unauthorized(new { success = false, error = "Usuario no autenticado." });
        }

        try
        {
            // Obtener el carrito desde la sesión
            var carritoSession = CarritoHelper.ObtenerCarrito(HttpContext.Session);
            if (carritoSession == null || !carritoSession.Any())
            {
                _logger.LogWarning("[FinalizarCompra] El carrito está vacío.");
                return BadRequest(new { success = false, error = "El carrito está vacío." });
            }

            // Calcular totales
            decimal total = carritoSession.Values.Sum(item => item.Precio * item.Cantidad);
            decimal subtotal = total / 1.15m;
            decimal iva = total - subtotal;

            _logger.LogInformation("[FinalizarCompra] Totales calculados: Subtotal = {Subtotal}, IVA = {IVA}, Total = {Total}.", subtotal, iva, total);

            // Generar un código único para el carrito
            string codigoCarrito = $"C-{await _context.Carritos.CountAsync() + 1:00000}";

            var nuevoCarrito = new Carrito
            {
                CarCodigo = codigoCarrito,
                CarSubtotal = subtotal,
                CarIva = iva,
                CarTotal = total,
                CarStatus = "ACT",
                CarFechaCreacion = DateTime.UtcNow
            };

            _logger.LogInformation("[FinalizarCompra] Carrito generado con código: {CodigoCarrito}.", codigoCarrito);

            _context.Carritos.Add(nuevoCarrito);
            await _context.SaveChangesAsync();

            // Obtener el ID del usuario autenticado
            var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioId))
            {
                _logger.LogError("[FinalizarCompra] No se pudo obtener el ID del usuario autenticado.");
                return BadRequest(new { success = false, error = "No se pudo identificar al usuario." });
            }

            var usuarioXCarrito = new UsuarioXCarrito
            {
                UsuarioId = usuarioId,
                CarritoId = nuevoCarrito.CarCodigo,
                UsuxcarStatus = "ACT",
                UsuxcarFechaModificacion = DateTime.UtcNow
            };

            _context.UsuarioXCarritos.Add(usuarioXCarrito);
            _logger.LogInformation("[FinalizarCompra] Asociación del carrito al usuario {UsuarioId} registrada.", usuarioId);

            // Procesar los ítems del carrito
            foreach (var item in carritoSession.Values)
            {
                if (item.Tipo == "libro")
                {
                    var libro = await _context.Libros.FirstOrDefaultAsync(l => l.LibCodigo == item.Id);
                    if (libro == null)
                    {
                        _logger.LogWarning("[FinalizarCompra] Libro con código {LibroId} no encontrado.", item.Id);
                        continue;
                    }

                    var libroXCarrito = new LibrosXCarrito
                    {
                        CarCodigo = nuevoCarrito.CarCodigo,
                        LibCodigo = libro.LibCodigo,
                        LibxcarCantidad = item.Cantidad,
                        LibxcarTotal = item.Precio * item.Cantidad
                    };

                    _context.LibrosXCarrito.Add(libroXCarrito);
                    _logger.LogInformation("[FinalizarCompra] Libro {LibroId} agregado al carrito.", item.Id);
                }
                else if (item.Tipo == "carta")
                {
                    var carta = await _context.Cartas.FirstOrDefaultAsync(c => c.CarCodigo == item.Id);
                    if (carta == null)
                    {
                        _logger.LogWarning("[FinalizarCompra] Carta con código {CartaId} no encontrada.", item.Id);
                        continue;
                    }

                    var cartaXCarrito = new CartaXCarrito
                    {
                        CarCodigo = nuevoCarrito.CarCodigo,
                        CartaCodigo = carta.CarCodigo,
                        CarxcarCantidad = item.Cantidad,
                        CarxcarTotal = item.Precio * item.Cantidad
                    };

                    _context.CartaXCarrito.Add(cartaXCarrito);
                    _logger.LogInformation("[FinalizarCompra] Carta {CartaId} agregada al carrito.", item.Id);
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("[FinalizarCompra] Compra finalizada y guardada en la base de datos.");

            // Limpiar el carrito en la sesión
            CarritoHelper.LimpiarCarrito(HttpContext.Session);
            _logger.LogInformation("[FinalizarCompra] Carrito en sesión limpiado.");

            return Ok(new { success = true, message = "Compra realizada con éxito." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[FinalizarCompra] Error al finalizar la compra.");
            return StatusCode(500, new { success = false, error = "Error al finalizar la compra." });
        }
    }
}