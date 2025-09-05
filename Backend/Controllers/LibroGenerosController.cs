using Backend.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Models;
using Service.ExtentionMethods;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroGenerosController : ControllerBase
    {
        private readonly BiblioContext _context;

        public LibroGenerosController(BiblioContext context)
        {
            _context = context;
        }

        // GET: api/LibroGeneros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroGenero>>> GetLibroGeneros([FromQuery] string filtro = "")
        {
            return await _context.LibroGeneros
                .Include(lg => lg.Libro)
                .Include(lg => lg.Genero)
                .AsNoTracking()
                .Where(lg => lg.Libro.Titulo.ToUpper().Contains(filtro.ToUpper()) ||
                             lg.Libro.Descripcion.ToUpper().Contains(filtro.ToUpper()) ||
                             lg.Genero.Nombre.ToUpper().Contains(filtro.ToUpper()))
                .ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<LibroGenero>>> GetDeletedsLibroGeneros()
        {
            return await _context.LibroGeneros
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(l => l.IsDeleted).ToListAsync();
        }

        // GET: api/LibroGeneros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroGenero>> GetLibroGenero(int id)
        {
            var libroGenero = await _context.LibroGeneros.AsNoTracking().FirstOrDefaultAsync(l=>l.Id.Equals(id));

            if (libroGenero == null)
            {
                return NotFound();
            }

            return libroGenero;
        }

        // PUT: api/LibroGeneros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibroGenero(int id, LibroGenero libroGenero)
        {
            _context.TryAttach(libroGenero?.Libro);
            _context.TryAttach(libroGenero?.Genero);
            if (id != libroGenero.Id)
            {
                return BadRequest();
            }

            _context.Entry(libroGenero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroGeneroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LibroGeneros
        [HttpPost]
        public async Task<ActionResult<LibroGenero>> PostLibroGenero(LibroGenero libroGenero)
        {
            _context.TryAttach(libroGenero?.Libro);
            _context.TryAttach(libroGenero?.Genero);
            _context.LibroGeneros.Add(libroGenero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibroGenero", new { id = libroGenero.Id }, libroGenero);
        }

        // DELETE: api/LibroGeneros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibroGenero(int id)
        {
            var libroGenero = await _context.LibroGeneros.FindAsync(id);
            if (libroGenero == null)
            {
                return NotFound();
            }
            libroGenero.IsDeleted=true;
            _context.LibroGeneros.Update(libroGenero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreLibroGenero(int id)
        {
            var libroGenero = await _context.LibroGeneros.IgnoreQueryFilters().FirstOrDefaultAsync(l=>l.Id.Equals(id));
            if (libroGenero == null)
            {
                return NotFound();
            }
            libroGenero.IsDeleted=false;
            _context.LibroGeneros.Update(libroGenero);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool LibroGeneroExists(int id)
        {
            return _context.LibroGeneros.Any(e => e.Id == id);
        }
    }
}
