using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/employes")]
public class EmployeController : ControllerBase
{
    private readonly BackendContext _context;
    public EmployeController(BackendContext context)
    {
        _context = context;
    }

    // GET: api/employes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeDTO>>> GetEmployes()
    {
        var employes = _context.Employes.Select(x => new EmployeDTO(x));

        return await employes.ToListAsync();
    }

    // GET: api/employe/2
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeDTO>> GetEmploye(int id)
    {
        var employe = await _context.Employes.SingleOrDefaultAsync(t => t.Id == id);

        if (employe == null)
            return NotFound();

        return new EmployeDTO(employe);
    }

    // POST: api/employe
    [HttpPost]
    public async Task<ActionResult<Employe>> PostEmploye(EmployeDTO employeDTO)
    {
        if (!IsMotDePasseValid(employeDTO.MotDePasse))
        {
            return BadRequest("Le mot de passe doit contenir au moins un caractère et un chiffre.");
        }

        Employe employe = new Employe(employeDTO, _context);

        employeDTO.MotDePasse = "";

        _context.Employes.Add(employe);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmploye), new { id = employe.Id }, new EmployeDTO(employe));
    }

    private bool IsMotDePasseValid(string motDePasse)
    {
        // Exemple de validation : Au moins un caractère et un chiffre
        return !string.IsNullOrWhiteSpace(motDePasse) && motDePasse.Any(char.IsLetter) && motDePasse.Any(char.IsDigit);
    }



    // DELETE: api/delete/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeItem(int id)
    {
        var employe = await _context.Employes.FindAsync(id);

        if (employe == null)
            return NotFound();

        _context.Employes.Remove(employe);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}