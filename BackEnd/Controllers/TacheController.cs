using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


[ApiController]
[Route("api/taches")]
public class TacheController : ControllerBase
{
    private readonly BackendContext _context;
    public TacheController(BackendContext context)
    {
        _context = context;
    }


    // GET: api/taches
    [Authorize(Roles = "Manager")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TacheDTO>>> GetTaches()
    {
        var taches = _context.Taches.Select(x => new TacheDTO(x));
        return await taches.ToListAsync();
    }


    // GET: api/tache/2
    [Authorize(Roles = "Manager")]
    [HttpGet("{id}")]
    public async Task<ActionResult<TacheDTO>> GetTache(int id)
    {
        var tache = await _context.Taches.SingleOrDefaultAsync(t => t.Id == id);

        if (tache == null)
            return NotFound();

        return new TacheDTO(tache);
    }


    // GET : api/taches/byEmploye/3
    [Authorize]
    [HttpGet("byEmploye/{idEmploye}")]
    public async Task<ActionResult<IEnumerable<TacheDTO>>> GetTachesByEmployeId(int idEmploye)
    {

        var assignations = await _context.Assignations
            .Where(a => a.IdEmploye == idEmploye)
            .ToListAsync();

        if (assignations == null || assignations.Count == 0)
            return NotFound();

        var tacheIds = assignations.Select(a => a.IdTache).ToList();

        var taches = await _context.Taches
            .Where(t => tacheIds.Contains(t.Id))
            .ToListAsync();

        if (taches == null || taches.Count == 0)
            return NotFound();

        return taches.Select(t => new TacheDTO(t)).ToList();
    }


    // POST: api/tache
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<ActionResult<Tache>> PostTache(TacheDTO tacheDTO)
    {
        Tache tache = new Tache(tacheDTO, _context);

        _context.Taches.Add(tache);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTache), new { id = tache.Id }, new TacheDTO(tache));
    }


    // DELETE: api/taches/2
    [Authorize(Roles = "Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTache(int id)
    {
        var tache = await _context.Taches.FindAsync(id);

        if (tache == null)
            return NotFound();

        var assignations = _context.Assignations.Where(a => a.IdTache == id);
        _context.Assignations.RemoveRange(assignations);

        _context.Taches.Remove(tache);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // PUT: api/tache/2
    [Authorize(Roles = "Manager")]
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTache(int id, TacheDTO tacheDTO)
    {
        if (id != tacheDTO.Id)
        {
            return BadRequest("L'ID de la tâche ne correspond pas à celui dans l'URL.");
        }

        var existingTache = await _context.Taches.FindAsync(id);

        if (existingTache == null)
        {
            return NotFound($"Aucune tâche trouvée avec l'ID {id}.");
        }

        existingTache.Duree = tacheDTO.Duree;
        existingTache.Description = tacheDTO.Description;
        existingTache.Echeance = tacheDTO.Echeance;

        _context.Entry(existingTache).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Taches.Any(e => e.Id == id))
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