using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/conge")]
public class CongeController : ControllerBase
{
    private readonly BackendContext _context;
    public CongeController(BackendContext context)
    {
        _context = context;
    }

    // GET: api/conges
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CongeDTO>>> GetConges()
    {
        var conges = _context.Conges.Select(x => new CongeDTO(x));

        return await conges.ToListAsync();
    }

    // GET: api/conge/2
    [Authorize]
    [HttpGet("conge/{id}")]
    public async Task<ActionResult<CongeDTO>> GetConge(int id)
    {
        var conge = await _context.Conges.SingleOrDefaultAsync(t => t.Id == id);

        if (conge == null)
            return NotFound();

        return new CongeDTO(conge);
    }

    // GET : api/conge/byEmploye/{hashedId}
    [Authorize]
    [HttpGet("byEmploye/{idEmploye}")]
    public async Task<ActionResult<IEnumerable<CongeDTO>>> GetCongesByEmployeId(int idEmploye)
    {
        var conges = await _context.Conges
            .Where(c => c.IdEmploye == idEmploye)
            .ToListAsync();

        if (conges == null || conges.Count == 0)
            return NotFound();

        return conges.Select(t => new CongeDTO(t)).ToList();
    }


    // POST: api/conge
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<ActionResult<Conge>> PostConge(CongeDTO congeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Conge conge = new Conge(congeDTO, _context);

        _context.Conges.Add(conge);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetConge), new { id = conge.Id }, new CongeDTO(conge));
    }


    // DELETE: api/conge/2
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConge(int id)
    {
        var conge = await _context.Conges.FindAsync(id);

        if (conge == null)
            return NotFound();

        _context.Conges.Remove(conge);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // PUT: api/conge/2
    [Authorize(Roles = "Manager")]
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutConge(int id, CongeDTO congeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != congeDTO.Id)
        {
            return BadRequest("L'ID du congé ne correspond pas à celui dans l'URL.");
        }

        var existingConge = await _context.Conges.FindAsync(id);

        if (existingConge == null)
        {
            return NotFound($"Aucun congé trouvé avec l'ID {id}.");
        }

        existingConge.IdEmploye = congeDTO.IdEmploye;
        existingConge.Duree = congeDTO.Duree;
        existingConge.Date = congeDTO.Date;
        existingConge.Motif = congeDTO.Motif;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Conges.Any(e => e.Id == id))
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
}