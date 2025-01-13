using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaStandFree.Models
{
    public class Usuario : IdentityUser
    {
        [Column("usuNombre")]
        [Display(Name = "Primer Nombre")]
        public string UsuNombre { get; set; } = string.Empty;

        [Column("usuApellido")]
        [Display(Name = "Primer Apellido")]
        public string UsuApellido { get; set; } = string.Empty;

        [Column("usuFechaNacimiento")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? UsuFechaNacimiento { get; set; }

        [Column("usuGenero")]
        [Display(Name = "Género")]
        public string UsuGenero { get; set; } = string.Empty;

        [Column("usuTelefono")]
        [Display(Name = "Teléfono")]
        public string UsuTelefono { get; set; } = string.Empty;

        [Column("usuApodo")]
        [Display(Name = "Apodo")]
        public string UsuApodo { get; set; } = string.Empty;

        [Column("usuPreferenciaAnuncios")]
        [Display(Name = "Preferencia de Anuncios")]
        public bool UsuPreferenciaAnuncios { get; set; } = false;

        [Column("usuStatus")]
        public string UsuStatus { get; set; } = "Activo";

        [Column("dateJoined")]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        [Column("lastLogin")]
        public DateTime? LastLogin { get; set; }

        // Relación con carritos
        public ICollection<UsuarioXCarrito> UsuarioXCarritos { get; set; } = new List<UsuarioXCarrito>();
    }
}