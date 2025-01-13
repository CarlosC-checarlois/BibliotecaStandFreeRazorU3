using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Models
{
    public class Carta
    {
        [Key]
        [Column("carCodigo")]
        [StringLength(7)] // Longitud máxima definida en Django
        public string CarCodigo { get; set; } // Clave primaria

        [Column("carNombre")]
        [StringLength(150)] // Longitud máxima definida en Django
        public string CarNombre { get; set; } // Nombre del producto

        [Column("carCantidad")]
        [Range(0, int.MaxValue)] // Valor mínimo de 0
        public int CarCantidad { get; set; } // Cantidad del producto

        [Column("carDescripcion")]
        public string CarDescripcion { get; set; } // Descripción del producto (sin límite de longitud)

        [Column("carPrecio")]
        [Range(0, 9999999.99)] // Rango de valores decimales
        [Precision(9, 2)] // Precisión del campo decimal (max_digits=9, decimal_places=2)
        public decimal CarPrecio { get; set; } // Precio del producto

        [Column("carFoto")]
        public string CarFoto { get; set; } // Ruta o URL de la imagen del producto

        [Column("carStatus")]
        [StringLength(3)] // Longitud máxima de 3 caracteres
        public string CarStatus { get; set; } = "ACT"; // Estado del producto (valor predeterminado: ACT)
        
        // Relación con categorías
        public ICollection<RelCartaCategoria> RelCartaCategorias { get; set; }
    }
}