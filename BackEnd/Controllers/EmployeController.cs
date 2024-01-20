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

    // GET : api/employe/3
    [HttpGet("byTache/{idTache}")]
    public async Task<ActionResult<IEnumerable<EmployeDTO>>> GetEmployesByTacheId(int idTache)
    {
        var affectations = await _context.Assignations
            .Where(a => a.IdTache == idTache)
            .ToListAsync();

        if (affectations == null || affectations.Count == 0)
            return NotFound();

        var employes = await _context.Employes
            .Where(e => affectations.Any(a => a.IdEmploye == e.Id))
            .ToListAsync();

        if (employes == null || employes.Count == 0)
            return NotFound();

        return employes.Select(e => new EmployeDTO(e)).ToList();
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

    // PUT: api/employe/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmploye(int id, EmployeDTO employeDTO)
    {
        if (id != employeDTO.Id)
            return BadRequest();

        // Récupérer l'employé existant
        Employe existingEmploye = await _context.Employes.FindAsync(id);

        if (existingEmploye == null)
            return NotFound();

        // Mise à jour des autres propriétés sans toucher au mot de passe
        existingEmploye.Nom = employeDTO.Nom;
        existingEmploye.Prenom = employeDTO.Prenom;
        existingEmploye.Email = employeDTO.Email;
        existingEmploye.SetStatut(employeDTO.Statut);
        existingEmploye.SetPole(employeDTO.Pole);

        // Mettre à jour l'employé dans le contexte et sauvegarder les modifications
        _context.Entry(existingEmploye).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Employes.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

}