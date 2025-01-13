using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaStandFree.Models
{
    public class LibrosXLibreriaCategoria
    {
        [Key]
        [Column("libxlibcatId")]
        public int LibxlibcatId { get; set; } // Clave primaria autogenerada

        [ForeignKey("Libro")]
        [Column("libroId")]
        [Required]
        public string LibroId { get; set; } // Clave foránea de Libro
        public Libro Libro { get; set; } // Relación con la clase Libro

        [ForeignKey("Categoria")]
        [Column("categoriaId")]
        [Required]
        public string CategoriaId { get; set; } // Clave foránea de LibroCategoria
        public LibroCategoria Categoria { get; set; } // Relación con la clase LibroCategoria
    }
}