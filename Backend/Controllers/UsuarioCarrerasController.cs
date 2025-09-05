using Backend.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Models;
using Service.ExtentionMethods;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioCarrerasController : ControllerBase
    {
        private readonly BiblioContext _context;

        public UsuarioCarrerasController(BiblioContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioCarreras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioCarrera>>> GetUsuarioCarreras([FromQuery] string filtro = "")
        {
            return await _context.UsuarioCarreras
                .Include(uc => uc.Usuario)
                .Include(uc => uc.Carrera)
                .AsNoTracking()
                .Where(uc => uc.Usuario.Nombre.ToUpper().Contains(filtro.ToUpper()) ||
                             uc.Carrera.Nombre.ToUpper().Contains(filtro.ToUpper()))
                .ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<UsuarioCarrera>>> GetDeletedsUsuarioCarreras()
        {
            return await _context.UsuarioCarreras
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(u => u.IsDeleted).ToListAsync();
        }

        // GET: api/UsuarioCarreras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioCarrera>> GetUsuarioCarrera(int id)
        {
            var usuarioCarrera = await _context.UsuarioCarreras.AsNoTracking().FirstOrDefaultAsync(u=>u.Id.Equals(id));

            if (usuarioCarrera == null)
            {
                return NotFound();
            }

            return usuarioCarrera;
        }

        // PUT: api/UsuarioCarreras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioCarrera(int id, UsuarioCarrera usuarioCarrera)
        {
            _context.TryAttach(usuarioCarrera?.Usuario);
            _context.TryAttach(usuarioCarrera?.Carrera);
            if (id != usuarioCarrera.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuarioCarrera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioCarreraExists(id))
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

        // POST: api/UsuarioCarreras
        [HttpPost]
        public async Task<ActionResult<UsuarioCarrera>> PostUsuarioCarrera(UsuarioCarrera usuarioCarrera)
        {
            _context.TryAttach(usuarioCarrera?.Usuario);
            _context.TryAttach(usuarioCarrera?.Carrera);
            _context.UsuarioCarreras.Add(usuarioCarrera);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioCarrera", new { id = usuarioCarrera.Id }, usuarioCarrera);
        }

        // DELETE: api/UsuarioCarreras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioCarrera(int id)
        {
            var usuarioCarrera = await _context.UsuarioCarreras.FindAsync(id);
            if (usuarioCarrera == null)
            {
                return NotFound();
            }
            usuarioCarrera.IsDeleted=true;
            _context.UsuarioCarreras.Update(usuarioCarrera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreUsuarioCarrera(int id)
        {
            var usuarioCarrera = await _context.UsuarioCarreras.IgnoreQueryFilters().FirstOrDefaultAsync(u=>u.Id.Equals(id));
            if (usuarioCarrera == null)
            {
                return NotFound();
            }
            usuarioCarrera.IsDeleted=false;
            _context.UsuarioCarreras.Update(usuarioCarrera);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UsuarioCarreraExists(int id)
        {
            return _context.UsuarioCarreras.Any(e => e.Id == id);
        }
    }
}
