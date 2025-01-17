using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Models
{
    public class Libro
    {
        [Key]
        [Column("libCodigo")]
        [StringLength(7)] // Longitud máxima de 7 caracteres
        public string LibCodigo { get; set; } // Código único del libro (clave primaria)

        [Column("libNombre")]
        [Required]
        [StringLength(150)] // Longitud máxima de 150 caracteres
        public string LibNombre { get; set; } // Nombre del libro

        [Column("libAutor")]
        [Required]
        [StringLength(150)] // Longitud máxima de 150 caracteres
        public string LibAutor { get; set; } // Autor del libro

        [Column("libCantidad")]
        [Range(0, int.MaxValue)] // Valor mínimo de 0
        public int LibCantidad { get; set; } // Cantidad de libros disponibles

        [Column("libFechaPublicacion")]
        [Required]
        public DateTime LibFechaPublicacion { get; set; } // Fecha de publicación

        [Column("libVolumen")]
        [Range(0, int.MaxValue)] // Valor mínimo de 0
        public int LibVolumen { get; set; } // Volumen del libro

        [Column("libSinopsis")]
        public string LibSinopsis { get; set; } // Sinopsis del libro
        
        [Column("libFoto")]
        public string LibFoto { get; set; } // Ruta o URL de la imagen del libro

        [Column("libStatus")]
        [StringLength(3)] // Longitud máxima de 3 caracteres
        public string LibStatus { get; set; } = "ACT"; // Estado del libro (valor predeterminado: ACT)

        [Column("libPrecio")]
        [Range(0, 9999999.99)]
        [Precision(9, 2)] // Precisión para valores decimales
        public decimal LibPrecio { get; set; } // Precio del libro (opcional)

        public override string ToString()
        {
            return LibNombre; // Representación como cadena
        }
    }
}
