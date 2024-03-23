using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/Holiday")]
public class HolidayController : ControllerBase
{
    private readonly BackendContext _context;
    public HolidayController(BackendContext context)
    {
        _context = context;
    }

    // GET: api/holidays
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HolidayDTO>>> GetHolidays()
    {
        var holidays = _context.Holidays.Select(x => new HolidayDTO(x));

        return await holidays.ToListAsync();
    }

    // GET: api/Holiday/2
    [Authorize]
    [HttpGet("Holiday/{id}")]
    public async Task<ActionResult<HolidayDTO>> GetHoliday(int id)
    {
        var holiday = await _context.Holidays.SingleOrDefaultAsync(t => t.Id == id);

        if (holiday == null)
            return NotFound();

        return new HolidayDTO(holiday);
    }

    // GET : api/Holiday/byEmploye/{hashedId}
    [Authorize]
    [HttpGet("byEmploye/{idEmploye}")]
    public async Task<ActionResult<IEnumerable<HolidayDTO>>> GetHolidaysByEmployeId(int idEmploye)
    {
        var holidays = await _context.Holidays
            .Where(c => c.IdEmployee == idEmploye)
            .ToListAsync();

        if (holidays == null || holidays.Count == 0)
            return NotFound();

        return holidays.Select(t => new HolidayDTO(t)).ToList();
    }


    // POST: api/Holiday
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<ActionResult<Holiday>> PostHoliday(HolidayDTO HolidayDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Holiday Holiday = new Holiday(HolidayDTO, _context);

        _context.Holidays.Add(Holiday);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHoliday), new { id = Holiday.Id }, new HolidayDTO(Holiday));
    }


    // DELETE: api/Holiday/2
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHoliday(int id)
    {
        var Holiday = await _context.Holidays.FindAsync(id);

        if (Holiday == null)
            return NotFound();

        _context.Holidays.Remove(Holiday);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // PUT: api/Holiday/2
    [Authorize(Roles = "Manager")]
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHoliday(int id, HolidayDTO HolidayDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != HolidayDTO.Id)
        {
            return BadRequest("L'ID du congé ne correspond pas à celui dans l'URL.");
        }

        var existingHoliday = await _context.Holidays.FindAsync(id);

        if (existingHoliday == null)
        {
            return NotFound($"Aucun congé trouvé avec l'ID {id}.");
        }

        existingHoliday.IdEmployee = HolidayDTO.IdEmployee;
        existingHoliday.Duration = HolidayDTO.Duration;
        existingHoliday.Date = HolidayDTO.Date;
        existingHoliday.Reason = HolidayDTO.Reason;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Holidays.Any(e => e.Id == id))
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