using Backend.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using Service.DTOs;
using Service.Interfaces;
using Service.Models;
using Service.Services;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibrosController : ControllerBase
    {
        private readonly BiblioContext _context;
        private GeminiController _geminiController;

        public LibrosController(BiblioContext context)
        {
            _context = context;
            _geminiController = new GeminiController();
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
            var sinopsisFloats = filter.ForSinopsis? await this._geminiController.CrearEmbeddingAsync(filter.SearchText ?? string.Empty): new float[0];
            var sinopsisEmbedding = filter.ForSinopsis ? new Vector(sinopsisFloats): null;
            var query = _context.Libros
                .Include(l => l.Editorial)
                .Include(l => l.LibrosAutores).ThenInclude(la=> la.Autor)
                .Include(l => l.LibrosGeneros).ThenInclude(lg=> lg.Genero)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchText)&&!filter.ForSinopsis)
            {
                var search = filter.SearchText.ToLower();
                query = query.Where(l =>
                    (filter.ForTitulo && l.Titulo.ToLower().Contains(search)) ||
                    (filter.ForAutor && l.LibrosAutores.Any(la => la.Autor.Nombre.ToLower().Contains(search))) ||
                    (filter.ForEditorial && l.Editorial.Nombre.ToLower().Contains(search)) ||
                    (filter.ForGenero && l.LibrosGeneros.Any(lg => lg.Genero.Nombre.ToLower().Contains(search)))
                );

            }

            if(!filter.ForSinopsis) return await query.ToListAsync();
            // Si se debe filtrar por sinopsis, calcular la similitud y ordenar
            query = query
                .Where(l => l.SinopsisEmbedding != null)
                .OrderBy(l => l.SinopsisEmbedding!.L2Distance(sinopsisEmbedding!))
                .Take(10); // Limitar a los 20 más similares
            var result= await query.ToListAsync();
            return result;
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
            var libro = await _context.Libros
                .Include(l => l.Editorial)
                .Include(l => l.LibrosAutores).ThenInclude(la => la.Autor)
                .Include(l => l.LibrosGeneros).ThenInclude(lg => lg.Genero)
                .AsNoTracking()
                .FirstOrDefaultAsync(l=>l.Id.Equals(id));

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
            var libroAnterior = await _context.Libros
                                .Include(l => l.Editorial)
                                .Include(l => l.LibrosAutores).ThenInclude(la => la.Autor)
                                .Include(l => l.LibrosGeneros).ThenInclude(lg => lg.Genero)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(l => l.Id.Equals(id));
            if (libroAnterior == null)
            {
                return NotFound();
            }
            

            //buscamos autores y generos nuevos
            var autoresNuevos = libro.LibrosAutores.Where(la => !libroAnterior.LibrosAutores.Any(aa => aa.AutorId == la.AutorId)).ToList();
            var generosNuevos = libro.LibrosGeneros.Where(lg => !libroAnterior.LibrosGeneros.Any(ga => ga.GeneroId == lg.GeneroId)).ToList();

            //buscamos autores y géneros eliminados
            var autoresEliminados = libroAnterior.LibrosAutores.Where(aa => !libro.LibrosAutores.Any(la => la.AutorId == aa.AutorId)).ToList();
            var generosEliminados = libroAnterior.LibrosGeneros.Where(ga => !libro.LibrosGeneros.Any(lg => lg.GeneroId == ga.GeneroId)).ToList();

            //actualizamos las listas
            foreach (var autor in autoresNuevos)
            {
                _context.LibroAutores.Add(new LibroAutor { LibroId = libro.Id, AutorId = autor.AutorId });
            }
            foreach (var genero in generosNuevos)
            {
                _context.LibroGeneros.Add(new LibroGenero { LibroId = libro.Id, GeneroId = genero.GeneroId });
            }
            //eliminamos los autores y géneros eliminados
            foreach (var autor in autoresEliminados)
            {
                var libroAutor = await _context.LibroAutores.FirstOrDefaultAsync(la => la.LibroId == libro.Id && la.AutorId == autor.AutorId);
                if (libroAutor != null)
                {
                    _context.LibroAutores.Remove(libroAutor);
                }
            }
            foreach (var genero in generosEliminados)
            {
                var libroGenero = await _context.LibroGeneros.FirstOrDefaultAsync(lg => lg.LibroId == libro.Id && lg.GeneroId == genero.GeneroId);
                if (libroGenero != null)
                {
                    _context.LibroGeneros.Remove(libroGenero);
                }
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
            var sinopsisFloats = await this._geminiController.CrearEmbeddingAsync(libro.Sinopsis ?? string.Empty);
            libro.SinopsisEmbedding = new Vector(sinopsisFloats);
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
