using Microsoft.AspNetCore.Mvc;
using BibliotecaStandFree.Data;
using BibliotecaStandFree.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BibliotecaStandFree.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionarCartasAPI : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GestionarCartasAPI(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GestionarCartasAPI
        [HttpGet]
        public async Task<IActionResult> GetCartas()
        {
            var cartas = await _context.Cartas.ToListAsync();
            return Ok(cartas);
        }

        // PUT: api/GestionarCartasAPI/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCarta(string id, [FromBody] Carta cartaEditada)
        {
            if (id != cartaEditada.CarCodigo)
                return BadRequest("El código de la carta no coincide.");

            var carta = await _context.Cartas.FindAsync(id);
            if (carta == null)
                return NotFound();

            carta.CarNombre = cartaEditada.CarNombre;
            carta.CarPrecio = cartaEditada.CarPrecio;
            carta.CarCantidad = cartaEditada.CarCantidad;

            _context.Entry(carta).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/GestionarCartasAPI/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCarta(string id)
        {
            var carta = await _context.Cartas.FindAsync(id);
            if (carta == null)
                return NotFound();

            _context.Cartas.Remove(carta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}