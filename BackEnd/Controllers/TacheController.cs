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

    // GET: api/taches
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TacheDTO>>> GetTaches()
    {
        var taches = _context.Taches.Select(x => new TacheDTO(x));
        return await taches.ToListAsync();
    }
}