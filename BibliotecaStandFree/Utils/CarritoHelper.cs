using BibliotecaStandFree.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public static class CarritoHelper
{
    // Serializar y guardar el carrito en la sesión
    public static void GuardarCarrito(ISession session, Dictionary<string, CarritoItem> carrito)
    {
        session.SetString("Carrito", JsonConvert.SerializeObject(carrito));
    }

    // Obtener el carrito de la sesión
    public static Dictionary<string, CarritoItem> ObtenerCarrito(ISession session)
    {
        var carritoJson = session.GetString("Carrito");
        return string.IsNullOrEmpty(carritoJson)
            ? new Dictionary<string, CarritoItem>() // Si no existe, devuelve un carrito vacío
            : JsonConvert.DeserializeObject<Dictionary<string, CarritoItem>>(carritoJson);
    }
}

// Modelo para un ítem en el carrito
public class CarritoItem
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public string Tipo { get; set; } // "libro" o "carta"
    public string Imagen { get; set; }
}