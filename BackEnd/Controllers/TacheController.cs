using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/taches")]
public class TacheController : ControllerBase
{
    private readonly BackendContext _context;
    public TacheController(BackendContext context)
    {
        _context = context;
    }

    // GET: api/tache
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TacheDTO>>> GetTaches()
    {
        var taches = _context.Taches.Select(x => new TacheDTO(x));
        return await taches.ToListAsync();
    }

    // GET: api/tache/2
    [HttpGet("{id}")]
    public async Task<ActionResult<TacheDTO>> GetTache(int id)
    {
        var tache = await _context.Taches.SingleOrDefaultAsync(t => t.Id == id);

        if (tache == null)
            return NotFound();

        return new TacheDTO(tache);
    }

    // GET : api/tache/3
    [HttpGet("byEmploye/{idEmploye}")]
    public async Task<ActionResult<IEnumerable<TacheDTO>>> GetEmployesByEmployeId(int idEmploye)
    {
        var affectations = await _context.Assignations
            .Where(a => a.IdEmploye == idEmploye)
            .ToListAsync();

        if (affectations == null || affectations.Count == 0)
            return NotFound();

        var taches = await _context.Taches
            .Where(t => affectations.Any(a => a.IdTache == t.Id))
            .ToListAsync();

        if (taches == null || taches.Count == 0)
            return NotFound();

        return taches.Select(t => new TacheDTO(t)).ToList();
    }

}