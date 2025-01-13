using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibliotecaStandFree.Models
{
    public class RelCartaCategoria
    {
        [Key]
        [Column("relCartaCategoriaId")]
        public int RelCartaCategoriaId { get; set; } // Clave primaria autogenerada

        [ForeignKey("Carta")]
        [Required]
        [Column("cartaId")]
        public string CartaId { get; set; } // Clave foránea de Carta
        public Carta Carta { get; set; } // Relación con la clase Carta

        [ForeignKey("Categoria")]
        [Required]
        [Column("categoriaId")]
        public string CategoriaId { get; set; } // Clave foránea de CartaCategoria
        public CartaCategoria Categoria { get; set; } // Relación con la clase CartaCategoria
    }

    // Configuración adicional para asegurar unicidad
    public class RelCartaCategoriaConfiguration : IEntityTypeConfiguration<RelCartaCategoria>
    {
        public void Configure(EntityTypeBuilder<RelCartaCategoria> builder)
        {
            builder.HasIndex(rc => new { rc.CartaId, rc.CategoriaId })
                .IsUnique(); // Define una restricción única para evitar duplicados
        }
    }
}