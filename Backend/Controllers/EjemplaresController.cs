using Backend.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Models;
using Service.ExtentionMethods;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjemplaresController : ControllerBase
    {
        private readonly BiblioContext _context;

        public EjemplaresController(BiblioContext context)
        {
            _context = context;
        }

        // GET: api/Ejemplares
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ejemplar>>> GetEjemplares()
        {
            return await _context.Ejemplares.AsNoTracking().ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Ejemplar>>> GetDeletedsEjemplares()
        {
            return await _context.Ejemplares
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(e => e.IsDeleted).ToListAsync();
        }

        // GET: api/Ejemplares/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ejemplar>> GetEjemplar(int id)
        {
            var ejemplar = await _context.Ejemplares.AsNoTracking().FirstOrDefaultAsync(e=>e.Id.Equals(id));

            if (ejemplar == null)
            {
                return NotFound();
            }

            return ejemplar;
        }

        // PUT: api/Ejemplares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEjemplar(int id, Ejemplar ejemplar)
        {
            _context.TryAttach(ejemplar?.Libro);
            if (id != ejemplar.Id)
            {
                return BadRequest();
            }

            _context.Entry(ejemplar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EjemplarExists(id))
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

        // POST: api/Ejemplares
        [HttpPost]
        public async Task<ActionResult<Ejemplar>> PostEjemplar(Ejemplar ejemplar)
        {
            _context.TryAttach(ejemplar?.Libro);
            _context.Ejemplares.Add(ejemplar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEjemplar", new { id = ejemplar.Id }, ejemplar);
        }

        // DELETE: api/Ejemplares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEjemplar(int id)
        {
            var ejemplar = await _context.Ejemplares.FindAsync(id);
            if (ejemplar == null)
            {
                return NotFound();
            }
            ejemplar.IsDeleted=true;
            _context.Ejemplares.Update(ejemplar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreEjemplar(int id)
        {
            var ejemplar = await _context.Ejemplares.IgnoreQueryFilters().FirstOrDefaultAsync(e=>e.Id.Equals(id));
            if (ejemplar == null)
            {
                return NotFound();
            }
            ejemplar.IsDeleted=false;
            _context.Ejemplares.Update(ejemplar);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EjemplarExists(int id)
        {
            return _context.Ejemplares.Any(e => e.Id == id);
        }
    }
}
