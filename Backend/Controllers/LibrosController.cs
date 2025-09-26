using Backend.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibrosController : ControllerBase
    {
        private readonly BiblioContext _context;

        public LibrosController(BiblioContext context)
        {
            _context = context;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros([FromQuery] string filtro="")
        {
            return await _context.Libros
                .Include(l => l.Editorial)
                .Include(l => l.LibrosAutores).ThenInclude(la => la.Autor)
                .Include(l => l.LibrosGeneros).ThenInclude(lg => lg.Genero)
                .AsNoTracking()
                .Where(l=>l.Titulo.Contains(filtro))
                .ToListAsync();
        }

        [HttpPost("withfilter")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibroswithfilter(FilterLibroDTO filter)
        {
            var query = _context.Libros
                .Include(l => l.Editorial)
                .Include(l => l.LibrosAutores).ThenInclude(la=> la.Autor)
                .Include(l => l.LibrosGeneros).ThenInclude(lg=> lg.Genero)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var search = filter.SearchText.ToLower();
                query = query.Where(l =>
                    (filter.ForTitulo && l.Titulo.ToLower().Contains(search)) ||
                    (filter.ForAutor && l.LibrosAutores.Any(la => la.Autor.Nombre.ToLower().Contains(search))) ||
                    (filter.ForEditorial && l.Editorial.Nombre.ToLower().Contains(search)) ||
                    (filter.ForGenero && l.LibrosGeneros.Any(lg => lg.Genero.Nombre.ToLower().Contains(search)))
                );

            }

            return await query.ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetDeletedsLibros()
        {
            return await _context.Libros
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(l => l.IsDeleted).ToListAsync();
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
            var libro = await _context.Libros.AsNoTracking().FirstOrDefaultAsync(l=>l.Id.Equals(id));

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        // PUT: api/Libros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(id))
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

        // POST: api/Libros
        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibro", new { id = libro.Id }, libro);
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            libro.IsDeleted=true;
            _context.Libros.Update(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreLibro(int id)
        {
            var libro = await _context.Libros.IgnoreQueryFilters().FirstOrDefaultAsync(l=>l.Id.Equals(id));
            if (libro == null)
            {
                return NotFound();
            }
            libro.IsDeleted=false;
            _context.Libros.Update(libro);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}
