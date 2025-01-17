using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BibliotecaStandFree.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    carCodigo = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    carSubtotal = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false),
                    carIva = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false),
                    carTotal = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false),
                    carFechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    carStatus = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.carCodigo);
                });

            migrationBuilder.CreateTable(
                name: "CartaCategorias",
                columns: table => new
                {
                    carxcatCodigo = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    carxcatNombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    carxcatStatus = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartaCategorias", x => x.carxcatCodigo);
                });

            migrationBuilder.CreateTable(
                name: "Cartas",
                columns: table => new
                {
                    carCodigo = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    carNombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    carCantidad = table.Column<int>(type: "integer", nullable: false),
                    carDescripcion = table.Column<string>(type: "text", nullable: false),
                    carPrecio = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false),
                    carFoto = table.Column<string>(type: "text", nullable: false),
                    carStatus = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartas", x => x.carCodigo);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    mensaje = table.Column<string>(type: "text", nullable: false),
                    fecha_envio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LibroCategorias",
                columns: table => new
                {
                    libxcatCodigo = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    libxcatNombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroCategorias", x => x.libxcatCodigo);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    libCodigo = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    libNombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    libAutor = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    libCantidad = table.Column<int>(type: "integer", nullable: false),
                    libFechaPublicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    libVolumen = table.Column<int>(type: "integer", nullable: false),
                    libSinopsis = table.Column<string>(type: "text", nullable: false),
                    libFoto = table.Column<string>(type: "text", nullable: false),
                    libStatus = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    libPrecio = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.libCodigo);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    usuNombre = table.Column<string>(type: "text", nullable: false),
                    usuApellido = table.Column<string>(type: "text", nullable: false),
                    usuFechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuGenero = table.Column<string>(type: "text", nullable: false),
                    usuTelefono = table.Column<string>(type: "text", nullable: false),
                    usuApodo = table.Column<string>(type: "text", nullable: false),
                    usuPreferenciaAnuncios = table.Column<bool>(type: "boolean", nullable: false),
                    usuStatus = table.Column<string>(type: "text", nullable: false),
                    dateJoined = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartaXCarrito",
                columns: table => new
                {
                    carxcarCodigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    carCodigo = table.Column<string>(type: "character varying(7)", nullable: false),
                    cartaCodigo = table.Column<string>(type: "character varying(7)", nullable: false),
                    carxcarCantidad = table.Column<int>(type: "integer", nullable: false),
                    carxcarTotal = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false),
                    CarritoCarCodigo = table.Column<string>(type: "character varying(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartaXCarrito", x => x.carxcarCodigo);
                    table.ForeignKey(
                        name: "FK_CartaXCarrito_Carritos_CarritoCarCodigo",
                        column: x => x.CarritoCarCodigo,
                        principalTable: "Carritos",
                        principalColumn: "carCodigo");
                    table.ForeignKey(
                        name: "FK_CartaXCarrito_Carritos_carCodigo",
                        column: x => x.carCodigo,
                        principalTable: "Carritos",
                        principalColumn: "carCodigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartaXCarrito_Cartas_cartaCodigo",
                        column: x => x.cartaCodigo,
                        principalTable: "Cartas",
                        principalColumn: "carCodigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelCartaCategorias",
                columns: table => new
                {
                    cartaId = table.Column<string>(type: "character varying(7)", nullable: false),
                    categoriaId = table.Column<string>(type: "character varying(7)", nullable: false),
                    relCartaCategoriaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelCartaCategorias", x => new { x.cartaId, x.categoriaId });
                    table.ForeignKey(
                        name: "FK_RelCartaCategorias_CartaCategorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "CartaCategorias",
                        principalColumn: "carxcatCodigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelCartaCategorias_Cartas_cartaId",
                        column: x => x.cartaId,
                        principalTable: "Cartas",
                        principalColumn: "carCodigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibrosXCarrito",
                columns: table => new
                {
                    libxcarCodigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    carCodigo = table.Column<string>(type: "character varying(7)", nullable: false),
                    libCodigo = table.Column<string>(type: "character varying(7)", nullable: false),
                    libxcarCantidad = table.Column<int>(type: "integer", nullable: false),
                    libxcarTotal = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false),
                    CarritoCarCodigo = table.Column<string>(type: "character varying(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibrosXCarrito", x => x.libxcarCodigo);
                    table.ForeignKey(
                        name: "FK_LibrosXCarrito_Carritos_CarritoCarCodigo",
                        column: x => x.CarritoCarCodigo,
                        principalTable: "Carritos",
                        principalColumn: "carCodigo");
                    table.ForeignKey(
                        name: "FK_LibrosXCarrito_Carritos_carCodigo",
                        column: x => x.carCodigo,
                        principalTable: "Carritos",
                        principalColumn: "carCodigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibrosXCarrito_Libros_libCodigo",
                        column: x => x.libCodigo,
                        principalTable: "Libros",
                        principalColumn: "libCodigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibrosXLibreriaCategorias",
                columns: table => new
                {
                    libroId = table.Column<string>(type: "character varying(7)", nullable: false),
                    categoriaId = table.Column<string>(type: "character varying(7)", nullable: false),
                    libxlibcatId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibrosXLibreriaCategorias", x => new { x.libroId, x.categoriaId });
                    table.ForeignKey(
                        name: "FK_LibrosXLibreriaCategorias_LibroCategorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "LibroCategorias",
                        principalColumn: "libxcatCodigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibrosXLibreriaCategorias_Libros_libroId",
                        column: x => x.libroId,
                        principalTable: "Libros",
                        principalColumn: "libCodigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioClaims_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UsuarioLogins_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UsuarioTokens_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioXCarritos",
                columns: table => new
                {
                    usuarioId = table.Column<string>(type: "text", nullable: false),
                    carritoId = table.Column<string>(type: "character varying(7)", nullable: false),
                    usuxcarStatus = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    usuxcarFechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioXCarritos", x => new { x.usuarioId, x.carritoId });
                    table.ForeignKey(
                        name: "FK_UsuarioXCarritos_Carritos_carritoId",
                        column: x => x.carritoId,
                        principalTable: "Carritos",
                        principalColumn: "carCodigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioXCarritos_Usuarios_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioXCarritos_Usuarios_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartaXCarrito_carCodigo",
                table: "CartaXCarrito",
                column: "carCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_CartaXCarrito_CarritoCarCodigo",
                table: "CartaXCarrito",
                column: "CarritoCarCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_CartaXCarrito_cartaCodigo",
                table: "CartaXCarrito",
                column: "cartaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_LibrosXCarrito_carCodigo",
                table: "LibrosXCarrito",
                column: "carCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_LibrosXCarrito_CarritoCarCodigo",
                table: "LibrosXCarrito",
                column: "CarritoCarCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_LibrosXCarrito_libCodigo",
                table: "LibrosXCarrito",
                column: "libCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_LibrosXLibreriaCategorias_categoriaId",
                table: "LibrosXLibreriaCategorias",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_RelCartaCategorias_categoriaId",
                table: "RelCartaCategorias",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioClaims_UserId",
                table: "UsuarioClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLogins_UserId",
                table: "UsuarioLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_RoleId",
                table: "UsuarioRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Usuarios",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Usuarios",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioXCarritos_carritoId",
                table: "UsuarioXCarritos",
                column: "carritoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioXCarritos_UsuarioId1",
                table: "UsuarioXCarritos",
                column: "UsuarioId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartaXCarrito");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "LibrosXCarrito");

            migrationBuilder.DropTable(
                name: "LibrosXLibreriaCategorias");

            migrationBuilder.DropTable(
                name: "RelCartaCategorias");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UsuarioClaims");

            migrationBuilder.DropTable(
                name: "UsuarioLogins");

            migrationBuilder.DropTable(
                name: "UsuarioRoles");

            migrationBuilder.DropTable(
                name: "UsuarioTokens");

            migrationBuilder.DropTable(
                name: "UsuarioXCarritos");

            migrationBuilder.DropTable(
                name: "LibroCategorias");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "CartaCategorias");

            migrationBuilder.DropTable(
                name: "Cartas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
