using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using Newtonsoft.Json;

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

        [HttpPost("agregar/{productoId}/{cantidad}/{tipo}")]
        public IActionResult AgregarAlCarrito(string productoId, int cantidad, string tipo)
        {
            // Obtiene el carrito de la sesión, o inicializa uno nuevo si no existe
            var carrito = HttpContext.Session.GetObjectFromJson<Dictionary<string, CarritoItem>>("Carrito") ?? new Dictionary<string, CarritoItem>();

            // Manejar productos según su tipo
            if (tipo == "libro")
            {
                var libro = _context.Libros.FirstOrDefault(l => l.LibCodigo == productoId);
                if (libro == null || libro.LibCantidad < cantidad)
                {
                    return BadRequest(new { mensaje = "El libro no existe o no hay suficiente stock." });
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

                libro.LibCantidad -= cantidad; // Reduce el stock
                _context.SaveChanges();
            }
            else if (tipo == "carta")
            {
                var carta = _context.Cartas.FirstOrDefault(c => c.CarCodigo == productoId);
                if (carta == null || carta.CarCantidad < cantidad)
                {
                    return BadRequest(new { mensaje = "La carta no existe o no hay suficiente stock." });
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

                carta.CarCantidad -= cantidad; // Reduce el stock
                _context.SaveChanges();
            }

            // Guarda el carrito en la sesión
            HttpContext.Session.SetObjectAsJson("Carrito", carrito);

            return Ok(new { mensaje = "Producto añadido al carrito.", carrito });
        }

        [HttpGet("ver")]
        public IActionResult VerCarrito()
        {
            var carrito = HttpContext.Session.GetObjectFromJson<Dictionary<string, CarritoItem>>("Carrito") ?? new Dictionary<string, CarritoItem>();
            return Ok(carrito);
        }
        
        [HttpDelete("eliminar/{productoId}/{tipoProducto}")]
        public IActionResult EliminarDelCarrito(string productoId, string tipoProducto)
        {
            // Obtener el carrito de la sesión
            var carrito = HttpContext.Session.GetObjectFromJson<Dictionary<string, CarritoItem>>("carrito") ?? new Dictionary<string, CarritoItem>();

            // Verificar si el producto existe en el carrito
            if (carrito.ContainsKey(productoId))
            {
                // Eliminar el producto del carrito
                carrito.Remove(productoId);

                // Actualizar el carrito en la sesión
                HttpContext.Session.SetObjectAsJson("carrito", carrito);

                return Ok(new { mensaje = "Producto eliminado del carrito exitosamente." });
            }

            return BadRequest(new { mensaje = "El producto no existe en el carrito." });
        }

    }

    public class CarritoItem
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }
        public string Imagen { get; set; }
    }
}
