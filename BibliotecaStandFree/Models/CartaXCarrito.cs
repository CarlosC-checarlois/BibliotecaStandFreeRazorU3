using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Models
{
    public class CartaXCarrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Clave primaria autogenerada
        [Column("carxcarCodigo")]
        public int CarxcarCodigo { get; set; } // Clave primaria autogenerada

        [ForeignKey("Carrito")]
        [Required]
        [Column("carCodigo")]
        public string CarCodigo { get; set; }
        public Carrito Carrito { get; set; }

        [ForeignKey("Carta")]
        [Required]
        [Column("cartaCodigo")]
        public string CartaCodigo { get; set; } // Clave foránea de Carta
        public Carta Carta { get; set; } // Relación con Carta

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        [Column("carxcarCantidad")]
        public int CarxcarCantidad { get; set; }
        
        [Required]
        [Range(0.01, 9999999.99, ErrorMessage = "El total debe ser mayor a 0.")]
        [Precision(9, 2)] // Precisión del campo decimal (max_digits=9, decimal_places=2)
        [Column("carxcarTotal")]
        public decimal CarxcarTotal { get; set; } // Total por este producto

        public override string ToString()
        {
            return $"{Carta?.CarNombre ?? "Desconocido"} x {CarxcarCantidad}";
        }
    }
}