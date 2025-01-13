using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaStandFree.Models
{
    public class Contacto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } // Clave primaria autogenerada

        [Column("nombre")]
        [Required]
        [StringLength(150)] // Longitud máxima de 150 caracteres
        public string Nombre { get; set; } // Nombre del contacto

        [Column("email")]
        [Required]
        [EmailAddress] // Validación para correos electrónicos
        public string Email { get; set; } // Correo electrónico del contacto

        [Column("mensaje")]
        [Required]
        public string Mensaje { get; set; } // Mensaje enviado por el contacto

        [Column("fecha_envio")]
        [Required]
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow; // Fecha de envío con valor predeterminado

        public override string ToString()
        {
            return $"Contacto de {Nombre} - {Email}";
        }
    }
}