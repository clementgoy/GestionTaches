using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/Holiday")]
public class HolidayController : ControllerBase
{
    private readonly BackendContext _context;

    // Initializes the controller with the database context
    public HolidayController(BackendContext context)
    {
        _context = context;
    }

    // GET: api/holidays
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HolidayDTO>>> GetHolidays()
    {
        // Projects each Holiday entity to a HolidayDTO
        var holidays = _context.Holidays.Select(x => new HolidayDTO(x));

        return await holidays.ToListAsync();
    }

    // GET: api/Holiday/2
    [Authorize]
    [HttpGet("Holiday/{id}")]
    public async Task<ActionResult<HolidayDTO>> GetHoliday(int id)
    {
        // Attempts to find the holiday by ID
        var holiday = await _context.Holidays.SingleOrDefaultAsync(t => t.Id == id);

        if (holiday == null)
            return NotFound();

        return new HolidayDTO(holiday);
    }

    // GET : api/Holiday/byEmploye/{Id}
    [Authorize]
    [HttpGet("byEmployee/{idEmployee}")]
    public async Task<ActionResult<IEnumerable<HolidayDTO>>> GetHolidaysByEmployeeId(int idEmployee)
    {
        // Fetches holidays that are linked to the specified employee ID
        var holidays = await _context.Holidays
            .Where(c => c.IdEmployee == idEmployee)
            .ToListAsync();

        if (holidays == null || holidays.Count == 0)
            return NotFound();

        return holidays.Select(t => new HolidayDTO(t)).ToList();
    }


    // POST: api/Holiday
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<ActionResult<Holiday>> PostHoliday(HolidayDTO holidayDTO)
    {
        // Validates the received model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Creates a new Holiday entity from the DTO and adds it to the context
        Holiday holiday = new Holiday(holidayDTO, _context);

        _context.Holidays.Add(holiday);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHoliday), new { id = holiday.Id }, new HolidayDTO(holiday));
    }


    // DELETE: api/Holiday/2
    [Authorize(Roles = "Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHoliday(int id)
    {
        var holiday = await _context.Holidays.FindAsync(id);

        if (holiday == null)
            return NotFound();

        _context.Holidays.Remove(holiday);
        await _context.SaveChangesAsync();

        return NoContent(); // Indicates successful deletion with no content in the response
    }


    // PUT: api/Holiday/2
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHoliday(int id, HolidayDTO holidayDTO)
    {
        // Validates the received model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Checks if the ID in the DTO matches the ID in the URL
        if (id != holidayDTO.Id)
        {
            return BadRequest("The holiday id does not match with the one in the URL");
        }

        // Attempts to find the existing holiday to update
        var existingHoliday = await _context.Holidays.FindAsync(id);

        if (existingHoliday == null)
        {
            return NotFound("Holiday not found");
        }

        // Updates the existing holiday's properties with values from the DTO
        existingHoliday.IdEmployee = holidayDTO.IdEmployee;
        existingHoliday.Duration = holidayDTO.Duration;
        existingHoliday.Date = holidayDTO.Date;
        existingHoliday.Reason = holidayDTO.Reason;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Checks if the holiday still exists
            if (!_context.Holidays.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw; // Re-throws the exception for unhandled cases
            }
        }

        return NoContent(); // Indicates successful update with no content in the response
    }
}