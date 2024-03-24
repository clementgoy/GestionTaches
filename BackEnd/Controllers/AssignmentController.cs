using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/assignment")]
public class AssignmentController : ControllerBase
{
    private readonly BackendContext _context;

    // Injects the database context through the constructor
    public AssignmentController(BackendContext context)
    {
        _context = context;
    }

    // GET: api/assignment/1
    [Authorize(Roles = "Manager")]
    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentDTO>> GetAssignment(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
        {
            return NotFound(); // Returns a 404 code if the assignment is not found
        }

        return new AssignmentDTO(assignment); // Converts the assignment to a DTO for the response
    }


    // POST: api/assignment
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<ActionResult<AssignmentDTO>> PostAssignment(AssignmentDTO assignmentDTO)
    {
        // Validates the request model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Additional checks to ensure that both the employee and the task exist
        if (!_context.Employees.Any(e => e.Id == assignmentDTO.IdEmployee))
        {
            return BadRequest("The employee does not exist");
        }

        if (!_context.Tasks.Any(t => t.Id == assignmentDTO.IdTask))
        {
            return BadRequest("The task does not exist");
        }

        var assignment = new Assignment(assignmentDTO, _context);
        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAssignment), new { id = assignment.Id }, new AssignmentDTO(assignment));
    }


    // PUT: api/assignment
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAssignment(int id, AssignmentDTO assignmentDTO)
    {
        // Validates the request model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != assignmentDTO.Id)
        {
            return BadRequest();
        }

        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
        {
            return NotFound();
        }

        if (!_context.Employees.Any(e => e.Id == assignmentDTO.IdEmployee))
        {
            return BadRequest("The employee does not exist");
        }

        if (!_context.Tasks.Any(t => t.Id == assignmentDTO.IdTask))
        {
            return BadRequest("The task does not exist");
        }

        assignment.Update(assignmentDTO);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Assignments.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent(); // Indicates that the operation was successful with no content in the response
    }


    // DELETE: api/assignment
    [Authorize(Roles = "Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
        {
            return NotFound();
        }

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();

        return NoContent(); // Indicates that the operation was successful with no content in the response
    }
}
