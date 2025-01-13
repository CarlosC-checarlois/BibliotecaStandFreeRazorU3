using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaStandFree.Utils
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            Console.WriteLine("=== Iniciando la creación de datos falsos ===");

            try
            {
                CreateLibroCategorias();
                CreateLibros(50);
                CreateCartaCategorias();
                CreateCartas(50);

                Console.WriteLine("=== Datos falsos creados exitosamente ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante la generación de datos: {ex.Message}");
            }
        }

        private void CreateLibroCategorias()
        {
            Console.WriteLine("Creando categorías de libros...");

            var categorias = new List<string>
            {
                "Ficción", "Ciencia", "Historia", "Fantasía", "Biografía", "Infantil", "Romance", "Misterio"
            };

            foreach (var categoria in categorias)
            {
                if (!_context.LibroCategorias.Any(c => c.LibxcatNombre == categoria))
                {
                    _context.LibroCategorias.Add(new LibroCategoria
                    {
                        LibxcatCodigo = new Faker().Random.AlphaNumeric(6).ToUpper(),
                        LibxcatNombre = categoria
                    });
                }
            }

            _context.SaveChanges();
            Console.WriteLine($"{categorias.Count} categorías de libros creadas.");
        }

        private void CreateLibros(int numLibros)
        {
            Console.WriteLine("Creando libros...");

            var faker = new Faker("es");
            var categorias = _context.LibroCategorias.ToList();

            for (int i = 0; i < numLibros; i++)
            {
                var libro = new Libro
                {
                    LibCodigo = faker.Random.AlphaNumeric(6).ToUpper(),
                    LibNombre = faker.Lorem.Sentence(4),
                    LibAutor = faker.Name.FullName(),
                    LibCantidad = faker.Random.Int(1, 5),
                    LibFechaPublicacion = faker.Date.Past(50),
                    LibVolumen = faker.Random.Int(1, 5),
                    LibSinopsis = faker.Lorem.Paragraph(),
                    LibURLLibro = faker.Internet.Url(),
                    LibFoto = faker.Image.PicsumUrl(),
                    LibStatus = "ACT",
                    LibPrecio = Math.Round(faker.Random.Decimal(5, 100), 2)
                };

                _context.Libros.Add(libro);

                // Asignar categorías aleatorias al libro
                if (categorias.Any())
                {
                    int numCategorias = faker.Random.Int(1, 2);
                    for (int j = 0; j < numCategorias; j++)
                    {
                        var categoria = faker.PickRandom(categorias);
                        if (!_context.LibrosXLibreriaCategorias.Any(lc => lc.LibroId == libro.LibCodigo && lc.CategoriaId == categoria.LibxcatCodigo))
                        {
                            _context.LibrosXLibreriaCategorias.Add(new LibrosXLibreriaCategoria
                            {
                                LibroId = libro.LibCodigo,
                                CategoriaId = categoria.LibxcatCodigo
                            });
                        }
                    }
                }
            }

            _context.SaveChanges();
            Console.WriteLine($"{numLibros} libros creados.");
        }

        private void CreateCartaCategorias()
        {
            Console.WriteLine("Creando categorías de carta...");

            var categorias = new List<string> { "Café", "Té", "Postres", "Snacks", "Bebidas Frías" };
            var faker = new Faker();

            foreach (var categoria in categorias)
            {
                if (!_context.CartaCategorias.Any(c => c.CarxcatNombre == categoria))
                {
                    _context.CartaCategorias.Add(new CartaCategoria
                    {
                        CarxcatCodigo = faker.Random.AlphaNumeric(6).ToUpper(),
                        CarxcatNombre = categoria,
                        CarxcatStatus = faker.Random.Bool() ? "ACT" : "INA"
                    });
                }
            }

            _context.SaveChanges();
            Console.WriteLine($"{categorias.Count} categorías de carta creadas.");
        }

        private void CreateCartas(int numCartas)
        {
            Console.WriteLine("Creando productos de carta...");

            var faker = new Faker("es");
            var categorias = _context.CartaCategorias.Where(c => c.CarxcatStatus == "ACT").ToList();

            for (int i = 0; i < numCartas; i++)
            {
                var carta = new Carta
                {
                    CarCodigo = faker.Random.AlphaNumeric(6).ToUpper(),
                    CarNombre = faker.Commerce.ProductName(),
                    CarCantidad = faker.Random.Int(1, 5),
                    CarDescripcion = faker.Lorem.Paragraph(),
                    CarPrecio = Math.Round(faker.Random.Decimal(1, 50), 2),
                    CarFoto = faker.Image.PicsumUrl(),
                    CarStatus = "ACT"
                };

                _context.Cartas.Add(carta);

                // Asignar categorías aleatorias al producto
                if (categorias.Any())
                {
                    int numCategorias = faker.Random.Int(1, 2);
                    for (int j = 0; j < numCategorias; j++)
                    {
                        var categoria = faker.PickRandom(categorias);
                        if (!_context.RelCartaCategorias.Any(rc => rc.CartaId == carta.CarCodigo && rc.CategoriaId == categoria.CarxcatCodigo))
                        {
                            _context.RelCartaCategorias.Add(new RelCartaCategoria
                            {
                                CartaId = carta.CarCodigo,
                                CategoriaId = categoria.CarxcatCodigo
                            });
                        }
                    }
                }
            }

            _context.SaveChanges();
            Console.WriteLine($"{numCartas} productos de carta creados.");
        }
    }
}
