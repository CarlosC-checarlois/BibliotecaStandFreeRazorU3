using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BibliotecaStandFree.Models;
using Microsoft.AspNetCore.Identity;

namespace BibliotecaStandFree.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
            
        }

        // DbSet para cada entidad personalizada
        public DbSet<Libro> Libros { get; set; }
        public DbSet<LibroCategoria> LibroCategorias { get; set; }
        public DbSet<LibrosXLibreriaCategoria> LibrosXLibreriaCategorias { get; set; }
        public DbSet<Carta> Cartas { get; set; }
        public DbSet<CartaCategoria> CartaCategorias { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<RelCartaCategoria> RelCartaCategorias { get; set; }
        public DbSet<LibrosXCarrito> LibrosXCarrito { get; set; }
        public DbSet<CartaXCarrito> CartaXCarrito { get; set; }
        public DbSet<UsuarioXCarrito> UsuarioXCarritos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de claves compuestas para LibrosXLibreriaCategoria
            modelBuilder.Entity<LibrosXLibreriaCategoria>()
                .HasKey(x => new { x.LibroId, x.CategoriaId });

            // Configuración de claves compuestas para RelCartaCategoria
            modelBuilder.Entity<RelCartaCategoria>()
                .HasKey(x => new { x.CartaId, x.CategoriaId });

            // Configuración de relaciones para LibrosXCarrito
            modelBuilder.Entity<LibrosXCarrito>()
                .HasKey(l => l.LibxcarCodigo); // Clave primaria

            modelBuilder.Entity<LibrosXCarrito>()
                .HasOne(l => l.Carrito)
                .WithMany()
                .HasForeignKey(l => l.CarCodigo)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<CartaXCarrito>()
                .HasOne(cx => cx.Carrito) // Relación con Carrito
                .WithMany()
                .HasForeignKey(cx => cx.CarCodigo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LibrosXCarrito>()
                .HasOne(l => l.Libro) // Relación con Libro
                .WithMany()
                .HasForeignKey(l => l.LibCodigo)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de relaciones para CartaXCarrito
            modelBuilder.Entity<CartaXCarrito>()
                .HasKey(cx => cx.CarxcarCodigo); // Clave primaria
            
            modelBuilder.Entity<CartaXCarrito>()
                .HasOne(cx => cx.Carta) // Relación con Carta
                .WithMany()
                .HasForeignKey(cx => cx.CartaCodigo)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de relaciones para UsuarioXCarrito
            modelBuilder.Entity<UsuarioXCarrito>()
                .HasKey(uxc => new { uxc.UsuarioId, uxc.CarritoId }); // Clave compuesta

            modelBuilder.Entity<UsuarioXCarrito>()
                .HasOne(uxc => uxc.Usuario) // Relación con Usuario
                .WithMany()
                .HasForeignKey(uxc => uxc.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioXCarrito>()
                .HasOne(uxc => uxc.Carrito) // Relación con Carrito
                .WithMany()
                .HasForeignKey(uxc => uxc.CarritoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Personalización de tablas de Identity
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsuarioRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsuarioClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsuarioLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsuarioTokens");
        }
    }
}
