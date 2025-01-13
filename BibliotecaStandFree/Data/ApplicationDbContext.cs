using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BibliotecaStandFree.Models;
using Microsoft.AspNetCore.Identity;

namespace BibliotecaStandFree.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de claves compuestas para tablas de relaciones
            modelBuilder.Entity<LibrosXLibreriaCategoria>()
                .HasKey(x => new { x.LibroId, x.CategoriaId });

            modelBuilder.Entity<RelCartaCategoria>()
                .HasKey(x => new { x.CartaId, x.CategoriaId });

            modelBuilder.Entity<LibrosXCarrito>()
                .HasKey(x => new { x.CarCodigo, x.LibCodigo });

            modelBuilder.Entity<CartaXCarrito>()
                .HasKey(x => new { x.CarCodigo, x.CartaCodigo });

            modelBuilder.Entity<UsuarioXCarrito>()
                .HasKey(x => new { x.UsuarioId, x.CarritoId });

            // Personaliza las tablas de Identity
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
