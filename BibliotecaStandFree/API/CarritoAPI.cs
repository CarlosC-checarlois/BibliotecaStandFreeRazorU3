using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using BibliotecaStandFree.Utils;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaStandFree.API
{
    [ApiController]
    [Route("api/carrito")]
    public class CarritoAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarritoAPI(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Agregar un producto al carrito
        /// </summary>
        /// <param name="productoId">ID del producto</param>
        /// <param name="cantidad">Cantidad</param>
        /// <param name="tipo">Tipo ("libro" o "carta")</param>
        [HttpPost("agregar/{productoId}/{cantidad}/{tipo}")]
        public IActionResult AgregarAlCarrito(string productoId, int cantidad, string tipo)
        {
            if (cantidad <= 0)
            {
                return BadRequest(new { mensaje = "La cantidad debe ser mayor a 0." });
            }

            // Obtener el carrito desde la sesión
            var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);

            if (tipo == "libro")
            {
                var libro = _context.Libros.FirstOrDefault(l => l.LibCodigo == productoId);
                if (libro == null)
                {
                    return BadRequest(new { mensaje = "El libro no existe." });
                }

                if (carrito.ContainsKey(productoId))
                {
                    carrito[productoId].Cantidad += cantidad;
                }
                else
                {
                    carrito[productoId] = new CarritoItem
                    {
                        Id = productoId,
                        Nombre = libro.LibNombre,
                        Precio = libro.LibPrecio,
                        Cantidad = cantidad,
                        Tipo = "libro",
                        Imagen = libro.LibFoto
                    };
                }
            }
            else if (tipo == "carta")
            {
                var carta = _context.Cartas.FirstOrDefault(c => c.CarCodigo == productoId);
                if (carta == null)
                {
                    return BadRequest(new { mensaje = "La carta no existe." });
                }

                if (carrito.ContainsKey(productoId))
                {
                    carrito[productoId].Cantidad += cantidad;
                }
                else
                {
                    carrito[productoId] = new CarritoItem
                    {
                        Id = productoId,
                        Nombre = carta.CarNombre,
                        Precio = carta.CarPrecio,
                        Cantidad = cantidad,
                        Tipo = "carta",
                        Imagen = carta.CarFoto
                    };
                }
            }
            else
            {
                return BadRequest(new { mensaje = "Tipo de producto no válido." });
            }

            // Guardar el carrito actualizado en la sesión
            CarritoHelper.GuardarCarrito(HttpContext.Session, carrito);

            return Ok(new { mensaje = "Producto añadido al carrito.", carrito });
        }

        /// <summary>
        /// Ver el contenido del carrito
        /// </summary>
        [HttpGet("ver")]
        public IActionResult VerCarrito()
        {
            var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);
            return Ok(carrito);
        }

        /// <summary>
        /// Eliminar un producto del carrito
        /// </summary>
        /// <param name="productoId">ID del producto a eliminar</param>
        [HttpDelete("eliminar/{productoId}")]
        public IActionResult EliminarDelCarrito(string productoId)
        {
            var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);

            if (carrito.ContainsKey(productoId))
            {
                carrito.Remove(productoId);
                CarritoHelper.GuardarCarrito(HttpContext.Session, carrito);

                return Ok(new { mensaje = "Producto eliminado del carrito.", carrito });
            }

            return BadRequest(new { mensaje = "El producto no existe en el carrito." });
        }

        /// <summary>
        /// Confirmar la compra y actualizar el stock
        /// </summary>
        [HttpPost("confirmar")]
        public IActionResult ConfirmarCompra()
        {
            var carrito = CarritoHelper.ObtenerCarrito(HttpContext.Session);

            if (!carrito.Any())
            {
                return BadRequest(new { mensaje = "El carrito está vacío." });
            }

            foreach (var item in carrito.Values)
            {
                if (item.Tipo == "libro")
                {
                    var libro = _context.Libros.FirstOrDefault(l => l.LibCodigo == item.Id);
                    if (libro == null || libro.LibCantidad < item.Cantidad)
                    {
                        return BadRequest(new { mensaje = $"No hay suficiente stock del libro: {item.Nombre}." });
                    }

                    libro.LibCantidad -= item.Cantidad; // Reducir stock
                }
                else if (item.Tipo == "carta")
                {
                    var carta = _context.Cartas.FirstOrDefault(c => c.CarCodigo == item.Id);
                    if (carta == null || carta.CarCantidad < item.Cantidad)
                    {
                        return BadRequest(new { mensaje = $"No hay suficiente stock de la carta: {item.Nombre}." });
                    }

                    carta.CarCantidad -= item.Cantidad; // Reducir stock
                }
            }

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            // Vaciar el carrito después de confirmar la compra
            CarritoHelper.GuardarCarrito(HttpContext.Session, new Dictionary<string, CarritoItem>());

            return Ok(new { mensaje = "Compra confirmada exitosamente." });
        }
    }
}
