using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using System.Threading.Tasks;

namespace BibliotecaStandFree.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionarLibrosAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GestionarLibrosAPI(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GestionarLibrosAPI
        [HttpGet]
        public async Task<IActionResult> GetLibros()
        {
            var libros = await _context.Libros.ToListAsync();
            return Ok(libros);
        }

        // PUT: api/GestionarLibrosAPI/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarLibro(string id, [FromBody] Libro libroEditado)
        {
            if (id != libroEditado.LibCodigo)
                return BadRequest("El código del libro no coincide.");

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
                return NotFound();

            libro.LibNombre = libroEditado.LibNombre;
            libro.LibAutor = libroEditado.LibAutor;
            libro.LibPrecio = libroEditado.LibPrecio;
            libro.LibCantidad = libroEditado.LibCantidad;

            _context.Entry(libro).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/GestionarLibrosAPI/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarLibro(string id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
                return NotFound();

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}