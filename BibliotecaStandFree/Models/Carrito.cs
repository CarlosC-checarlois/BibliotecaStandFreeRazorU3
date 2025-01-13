using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Models
{
    public class Carrito
    {
        [Key]
        [Column("carCodigo")]
        [StringLength(7)] // Longitud máxima según el modelo de Django
        public string CarCodigo { get; set; } // Clave primaria

        [Column("carSubtotal")]
        [Range(0, 9999999.99)] // Rango para valores decimales
        [Precision(9, 2)] // Precisión del campo decimal (max_digits=9, decimal_places=2)
        public decimal CarSubtotal { get; set; }

        [Column("carIva")]
        [Range(0, 9999999.99)]
        [Precision(9, 2)]
        public decimal CarIva { get; set; }

        [Column("carTotal")]
        [Range(0, 9999999.99)]
        [Precision(9, 2)]
        public decimal CarTotal { get; set; }

        [Column("carStatus")]
        [StringLength(3)] // Longitud máxima de 3 caracteres
        public string CarStatus { get; set; } = "ACT"; // Valor predeterminado "ACT"

        // Relaciones con otras tablas
        public ICollection<LibrosXCarrito> LibrosXCarritos { get; set; }
        public ICollection<CartaXCarrito> CartaXCarritos { get; set; }
    }
}