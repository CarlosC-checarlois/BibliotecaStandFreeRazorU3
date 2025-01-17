using BibliotecaStandFree.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaStandFree.Utils
{
    public static class CarritoHelper
    {
        private const string CarritoKey = "Carrito";

        /// <summary>
        /// Serializar y guardar el carrito en la sesión.
        /// </summary>
        public static void GuardarCarrito(ISession session, Dictionary<string, CarritoItem> carrito)
        {
            session.SetString(CarritoKey, JsonConvert.SerializeObject(carrito));
        }

        /// <summary>
        /// Obtener el carrito de la sesión. Si no existe, devuelve un carrito vacío.
        /// </summary>
        public static Dictionary<string, CarritoItem> ObtenerCarrito(ISession session)
        {
            var carritoJson = session.GetString(CarritoKey);
            if (string.IsNullOrEmpty(carritoJson))
            {
                return new Dictionary<string, CarritoItem>();
            }

            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, CarritoItem>>(carritoJson);
            }
            catch
            {
                // En caso de error al deserializar, devolver un carrito vacío
                return new Dictionary<string, CarritoItem>();
            }
        }

        /// <summary>
        /// Agregar un ítem al carrito. Si ya existe, incrementa su cantidad.
        /// </summary>
        public static void AgregarItem(ISession session, CarritoItem item)
        {
            if (item.Cantidad <= 0)
            {
                // No permitir agregar cantidades negativas o cero
                return;
            }

            var carrito = ObtenerCarrito(session);

            if (carrito.ContainsKey(item.Id))
            {
                // Si el ítem ya existe, aumentar la cantidad
                carrito[item.Id].Cantidad += item.Cantidad;
            }
            else
            {
                // Si no existe, agregar el ítem
                carrito[item.Id] = item;
            }

            GuardarCarrito(session, carrito);
        }

        /// <summary>
        /// Eliminar un ítem del carrito.
        /// </summary>
        public static void EliminarItem(ISession session, string itemId)
        {
            var carrito = ObtenerCarrito(session);

            if (carrito.ContainsKey(itemId))
            {
                carrito.Remove(itemId);
                GuardarCarrito(session, carrito);
            }
        }

        /// <summary>
        /// Actualizar la cantidad de un ítem en el carrito.
        /// Si la nueva cantidad es 0 o menos, el ítem se elimina.
        /// </summary>
        public static void ActualizarCantidad(ISession session, string itemId, int nuevaCantidad)
        {
            if (nuevaCantidad < 0)
            {
                // No permitir cantidades negativas
                return;
            }

            var carrito = ObtenerCarrito(session);

            if (carrito.ContainsKey(itemId))
            {
                if (nuevaCantidad == 0)
                {
                    // Si la nueva cantidad es 0, eliminar el ítem
                    carrito.Remove(itemId);
                }
                else
                {
                    // Actualizar la cantidad
                    carrito[itemId].Cantidad = nuevaCantidad;
                }

                GuardarCarrito(session, carrito);
            }
        }

        /// <summary>
        /// Obtener el total de ítems (cantidad acumulada) en el carrito.
        /// </summary>
        public static int ObtenerTotalItems(ISession session)
        {
            var carrito = ObtenerCarrito(session);
            return carrito.Values.Sum(item => item.Cantidad);
        }

        /// <summary>
        /// Obtener el precio total del carrito.
        /// </summary>
        public static decimal ObtenerPrecioTotal(ISession session)
        {
            var carrito = ObtenerCarrito(session);
            return carrito.Values.Sum(item => item.Precio * item.Cantidad);
        }

        /// <summary>
        /// Limpiar el carrito (eliminar todos los ítems).
        /// </summary>
        public static void LimpiarCarrito(ISession session)
        {
            GuardarCarrito(session, new Dictionary<string, CarritoItem>());
        }
    }

    /// <summary>
    /// Modelo para un ítem en el carrito.
    /// </summary>
    public class CarritoItem
    {
        public string Id { get; set; } // Puede ser el ID del libro o carta
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; } // "libro" o "carta"
        public string Imagen { get; set; } // Ruta de la imagen del ítem
    }
}
