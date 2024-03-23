using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text;

[ApiController]
[Route("api/assignment")]
public class AssignmentController : ControllerBase
{
    private readonly BackendContext _context;

    public AssignmentController(BackendContext context)
    {
        _context = context;
    }

    private string HashId(int id)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(id.ToString()));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    // GET: api/assignment/1
    [Authorize(Roles = "Manager")]
    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentDTO>> GetAssignment(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
        {
            return NotFound();
        }

        return new AssignmentDTO(assignment);
    }


    // POST: api/assignment
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<ActionResult<AssignmentDTO>> PostAssignment(AssignmentDTO assignmentDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!_context.Employees.Any(e => e.Id == assignmentDTO.IdEmployee))
        {
            return BadRequest("L'employé spécifié n'existe pas.");
        }

        if (!_context.Tasks.Any(t => t.Id == assignmentDTO.IdTask))
        {
            return BadRequest("La tâche spécifiée n'existe pas.");
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
            return BadRequest("L'employé spécifié n'existe pas.");
        }

        if (!_context.Tasks.Any(t => t.Id == assignmentDTO.IdTask))
        {
            return BadRequest("La tâche spécifiée n'existe pas.");
        }

        assignment.Update(assignmentDTO);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AssignmentExists(id))
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

        return NoContent();
    }

    private bool AssignmentExists(int id)
    {
        return _context.Assignments.Any(e => e.Id == id);
    }
}
