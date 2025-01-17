using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Models
{
    public class LibrosXCarrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configurar clave primaria autogenerada
        [Column("libxcarCodigo")]
        public int LibxcarCodigo { get; set; } // Clave primaria autogenerada

        [ForeignKey("Carrito")]
        [Required]
        [Column("carCodigo")]
        public string CarCodigo { get; set; }
        public Carrito Carrito { get; set; }
        
        [ForeignKey("Libro")]
        [Required]
        [Column("libCodigo")]
        public string LibCodigo { get; set; } // Clave foránea de Libro
        public Libro Libro { get; set; } // Relación con Libro

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        [Column("libxcarCantidad")]
        public int LibxcarCantidad { get; set; } // Cantidad del libro en el carrito

        [Required]
        [Range(0.01, 9999999.99, ErrorMessage = "El total debe ser mayor a 0.")]
        [Precision(9, 2)] // Precisión del campo decimal
        [Column("libxcarTotal")]
        public decimal LibxcarTotal { get; set; } // Total por este libro

        public override string ToString()
        {
            return $"{Libro?.LibNombre ?? "Desconocido"} x {LibxcarCantidad}";
        }
    }
}