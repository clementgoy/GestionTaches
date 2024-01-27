using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

[ApiController]
[Route("api/assignation")]
public class AssignationController : ControllerBase
{
    private readonly BackendContext _context;

    public AssignationController(BackendContext context)
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

    // GET: api/assignation/1
    [HttpGet("{id}")]
    public async Task<ActionResult<AssignationDTO>> GetAssignation(int id)
    {
        var assignation = await _context.Assignations.FindAsync(id);

        if (assignation == null)
        {
            return NotFound();
        }

        return new AssignationDTO(assignation);
    }


    // POST: api/assignation
    [HttpPost]
    public async Task<ActionResult<AssignationDTO>> PostAssignation(AssignationDTO assignationDTO)
    {
        if (!_context.Employes.Any(e => e.Id == assignationDTO.IdEmploye))
        {
            return BadRequest("L'employé spécifié n'existe pas.");
        }

        if (!_context.Taches.Any(t => t.Id == assignationDTO.IdTache))
        {
            return BadRequest("La tâche spécifiée n'existe pas.");
        }

        var assignation = new Assignation(assignationDTO, _context);
        _context.Assignations.Add(assignation);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAssignation), new { id = assignation.Id }, new AssignationDTO(assignation));
    }


    // PUT: api/assignation
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAssignation(int id, AssignationDTO assignationDTO)
    {
        if (id != assignationDTO.Id)
        {
            return BadRequest();
        }

        var assignation = await _context.Assignations.FindAsync(id);

        if (assignation == null)
        {
            return NotFound();
        }

        if (!_context.Employes.Any(e => e.Id == assignationDTO.IdEmploye))
        {
            return BadRequest("L'employé spécifié n'existe pas.");
        }

        if (!_context.Taches.Any(t => t.Id == assignationDTO.IdTache))
        {
            return BadRequest("La tâche spécifiée n'existe pas.");
        }

        assignation.Update(assignationDTO);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AssignationExists(id))
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

    // DELETE: api/assignation
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignation(int id)
    {
        var assignation = await _context.Assignations.FindAsync(id);

        if (assignation == null)
        {
            return NotFound();
        }

        _context.Assignations.Remove(assignation);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AssignationExists(int id)
    {
        return _context.Assignations.Any(e => e.Id == id);
    }
}
