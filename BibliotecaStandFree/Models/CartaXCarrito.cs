using Microsoft.EntityFrameworkCore;
namespace BibliotecaStandFree.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartaXCarrito
{
    [Key]
    [Column("carxcarId")]
    public int CarxcarId { get; set; } // Clave primaria autogenerada

    [ForeignKey("Carrito")]
    [Column("carCodigo")]
    public string CarCodigo { get; set; } // Clave foránea de Carrito
    public Carrito Carrito { get; set; } // Relación con Carrito

    [ForeignKey("Carta")]
    [Column("cartaCodigo")]
    public string CartaCodigo { get; set; } // Clave foránea de Carta
    public Carta Carta { get; set; } // Relación con Carta

    [Column("carxcarCantidad")]
    [Range(0, int.MaxValue)] // Valor mínimo de 0
    public int CarxcarCantidad { get; set; } // Cantidad del producto en la carta

    [Column("carxcarTotal")]
    [Range(0, 9999999.99)] // Rango válido para valores decimales
    [Precision(9, 2)] // Precisión del campo decimal (max_digits=9, decimal_places=2)
    public decimal CarxcarTotal { get; set; } // Total por este producto

    public override string ToString()
    {
        return $"{Carta?.CarNombre} x {CarxcarCantidad}"; // Representación como cadena
    }
}