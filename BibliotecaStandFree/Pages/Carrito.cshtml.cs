using BibliotecaStandFree.Utils;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace BibliotecaStandFree.Pages
{
    public class CarritoModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarritoModel> _logger;

        public CarritoModel(ApplicationDbContext context, ILogger<CarritoModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Propiedad pública para pasar los datos del carrito a la vista
        public List<CarritoItem> Carrito { get; set; } = new();

        public IActionResult OnGet()
        {
            // Obtener el carrito desde la sesión
            Carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session).Values.ToList();
            return Page();
        }

        public IActionResult OnPostEliminar(string id, string tipo)
        {
            // Obtener el carrito desde la sesión
            var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);

            if (carrito.ContainsKey(id))
            {
                carrito.Remove(id); // Eliminar el producto del carrito
                CarritoHelper.GuardarCarrito(HttpContext.Session, carrito); // Actualizar la sesión
            }

            return RedirectToPage(); // Recargar la página
        }

        public async Task<IActionResult> OnPostFinalizarCompraAsync()
        {
            Console.WriteLine("[OnPostFinalizarCompra] Inicio del proceso de finalización de compra.");

            // Verificar si el usuario está autenticado
            if (!User.Identity?.IsAuthenticated ?? false)
            {
                Console.WriteLine("[OnPostFinalizarCompra] Usuario no autenticado. Redirigiendo al login.");
                return RedirectToPage("/Login");
            }

            // Obtener el carrito desde la sesión
            var carritoSession = CarritoHelper.ObtenerCarrito(HttpContext.Session);
            if (!carritoSession.Any())
            {
                TempData["Error"] = "El carrito está vacío.";
                Console.WriteLine("[OnPostFinalizarCompra] El carrito está vacío.");
                return RedirectToPage();
            }

            try
            {
                // Calcular totales
                decimal total = carritoSession.Values.Sum(item => item.Precio * item.Cantidad);
                decimal subtotal = total / 1.15m;
                decimal iva = total - subtotal;

                Console.WriteLine(
                    $"[OnPostFinalizarCompra] Totales calculados: Subtotal = {subtotal}, IVA = {iva}, Total = {total}.");

                // Generar un código único para el carrito
                string codigoCarrito = $"C-{await _context.Carritos.CountAsync() + 1:00000}";

                // Crear un nuevo carrito
                var nuevoCarrito = new Carrito
                {
                    CarCodigo = codigoCarrito,
                    CarSubtotal = subtotal,
                    CarIva = iva,
                    CarTotal = total,
                    CarStatus = "ACT",
                    CarFechaCreacion = DateTime.UtcNow
                };

                // Insertar el nuevo carrito en la base de datos
                _context.Carritos.Add(nuevoCarrito);
                await _context.SaveChangesAsync();
                Console.WriteLine($"[OnPostFinalizarCompra] Nuevo carrito creado con código {codigoCarrito}.");

                // Obtener el ID del usuario autenticado
                var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(usuarioId))
                {
                    TempData["Error"] = "No se pudo identificar al usuario.";
                    Console.WriteLine("[OnPostFinalizarCompra] No se pudo obtener el ID del usuario autenticado.");
                    return RedirectToPage();
                }

                // Asociar el carrito al usuario
                var usuarioXCarrito = new UsuarioXCarrito
                {
                    UsuarioId = usuarioId,
                    CarritoId = nuevoCarrito.CarCodigo,
                    UsuxcarStatus = "ACT",
                    UsuxcarFechaModificacion = DateTime.UtcNow
                };

                // Insertar la relación Usuario-Carrito
                _context.UsuarioXCarritos.Add(usuarioXCarrito);

                Console.WriteLine("[OnPostFinalizarCompra] Asociación del carrito al usuario creada.");

                // Procesar los ítems del carrito y realizar los INSERT
                foreach (var item in carritoSession.Values)
                {
                    if (item.Tipo == "carta")
                    {
                        // Buscar la carta en la base de datos
                        var carta = await _context.Cartas.FirstOrDefaultAsync(c => c.CarCodigo == item.Id);
                        if (carta == null)
                        {
                            Console.WriteLine($"[OnPostFinalizarCompra] Carta con código {item.Id} no encontrada.");
                            continue; // Saltar este ítem si no se encuentra
                        }

                        // Crear la relación CartaXCarrito
                        var cartaXCarrito = new CartaXCarrito
                        {
                            CarCodigo = nuevoCarrito.CarCodigo,
                            CartaCodigo = carta.CarCodigo,
                            CarxcarCantidad = item.Cantidad,
                            CarxcarTotal = item.Precio * item.Cantidad
                        };

                        // Insertar en la base de datos
                        _context.CartaXCarrito.Add(cartaXCarrito);
                        Console.WriteLine($"[OnPostFinalizarCompra] Carta añadida al carrito: {carta.CarCodigo}.");
                    }
                    else if (item.Tipo == "libro")
                    {
                        // Buscar el libro en la base de datos
                        var libro = await _context.Libros.FirstOrDefaultAsync(l => l.LibCodigo == item.Id);
                        if (libro == null)
                        {
                            Console.WriteLine($"[OnPostFinalizarCompra] Libro con código {item.Id} no encontrado.");
                            continue; // Saltar este ítem si no se encuentra
                        }

                        // Crear la relación LibrosXCarrito
                        var libroXCarrito = new LibrosXCarrito
                        {
                            CarCodigo = nuevoCarrito.CarCodigo,
                            LibCodigo = libro.LibCodigo,
                            LibxcarCantidad = item.Cantidad,
                            LibxcarTotal = item.Precio * item.Cantidad
                        };

                        // Insertar en la base de datos
                        _context.LibrosXCarrito.Add(libroXCarrito);
                        Console.WriteLine($"[OnPostFinalizarCompra] Libro añadido al carrito: {libro.LibCodigo}.");
                    }
                }

                // Guardar todos los cambios en la base de datos
                await _context.SaveChangesAsync();
                Console.WriteLine("[OnPostFinalizarCompra] Compra finalizada y guardada en la base de datos.");

                // Limpiar el carrito en la sesión
                CarritoHelper.LimpiarCarrito(HttpContext.Session);
                Console.WriteLine("[OnPostFinalizarCompra] Carrito en sesión limpiado.");

                TempData["Success"] = "¡Compra realizada con éxito!";
                return RedirectToPage("/Usuario/Panel");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[OnPostFinalizarCompra] Error al finalizar la compra: {ex.Message}");
                _logger.LogError(ex, "[OnPostFinalizarCompra] Error al finalizar la compra.");
                TempData["Error"] = "Ocurrió un error al finalizar la compra. Por favor, inténtalo de nuevo.";
                return RedirectToPage();
            }
        }
    }
}