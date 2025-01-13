using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaStandFree.Models
{
    public class CartaCategoria
    {
        [Key]
        [Column("carxcatCodigo")]
        [StringLength(7)] // Longitud máxima definida en Django
        public string CarxcatCodigo { get; set; } // Código de la categoría (clave primaria)

        [Column("carxcatNombre")]
        [StringLength(150)] // Longitud máxima definida en Django
        public string CarxcatNombre { get; set; } // Nombre de la categoría

        [Column("carxcatStatus")]
        [StringLength(3)] // Longitud máxima definida en Django
        public string CarxcatStatus { get; set; } // Estado de la categoría

        public override string ToString()
        {
            return CarxcatNombre; // Representación como cadena, equivalente a `__str__` en Django
        }
    }
}