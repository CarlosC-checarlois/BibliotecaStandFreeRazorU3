using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaStandFree.Models
{
    public class LibroCategoria
    {
        [Key]
        [Column("libxcatCodigo")]
        [StringLength(7)] // Longitud máxima definida en Django
        public string LibxcatCodigo { get; set; } // Código de la categoría (clave primaria)

        [Column("libxcatNombre")]
        [Required]
        [StringLength(150)] // Longitud máxima definida en Django
        public string LibxcatNombre { get; set; } // Nombre de la categoría

        public override string ToString()
        {
            return LibxcatNombre; // Representación como cadena, equivalente a `__str__` en Django
        }
    }
}