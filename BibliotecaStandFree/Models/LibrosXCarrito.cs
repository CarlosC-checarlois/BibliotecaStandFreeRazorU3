using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Models
{
    public class LibrosXCarrito
    {
        [Key]
        [Column("libxcarId")]
        public int LibxcarId { get; set; } // Clave primaria autogenerada

        [ForeignKey("Carrito")]
        [Column("carCodigo")]
        public string CarCodigo { get; set; } // Clave foránea de Carrito
        public Carrito Carrito { get; set; } // Relación con Carrito

        [ForeignKey("Libro")]
        [Column("libCodigo")]
        public string LibCodigo { get; set; } // Clave foránea de Libro
        public Libro Libro { get; set; } // Relación con Libro

        [Column("libxcarCantidad")]
        [Range(1, int.MaxValue)] // Valor mínimo de 1
        public int LibxcarCantidad { get; set; } // Cantidad del libro en el carrito

        [Column("libxcarTotal")]
        [Range(0, 9999999.99)] // Rango válido para valores decimales
        [Precision(9, 2)] // Precisión del campo decimal (max_digits=9, decimal_places=2)
        public decimal LibxcarTotal { get; set; } // Total por este libro

        public override string ToString()
        {
            return $"{Libro?.LibNombre} x {LibxcarCantidad}"; // Representación como cadena
        }
    }
}
