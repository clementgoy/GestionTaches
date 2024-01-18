using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CongeDTO>>> GetConges()
    {
        var conges = _context.Conges.Select(x => new CongeDTO(x));

        return await conges.ToListAsync();
    }
}