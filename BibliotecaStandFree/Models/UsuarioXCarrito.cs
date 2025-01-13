using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaStandFree.Models
{
    public class UsuarioXCarrito
    {
        [Key]
        [Column("usuarioId")]
        [Required]
        public string UsuarioId { get; set; } // Clave foránea hacia Usuario
        public Usuario Usuario { get; set; }

        [Key]
        [Column("carritoId")]
        [Required]
        public string CarritoId { get; set; } // Clave foránea hacia Carrito
        public Carrito Carrito { get; set; }

        [Column("usuxcarStatus")]
        [StringLength(3)] // Limita la longitud del estado a 3 caracteres
        [Required]
        public string UsuxcarStatus { get; set; } // Estado de la relación (ejemplo: ACT)

        [Column("usuxcarFechaModificacion")]
        [Required]
        public DateTime UsuxcarFechaModificacion { get; set; } // Fecha de última modificación
    }
}