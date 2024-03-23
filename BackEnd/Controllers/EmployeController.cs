using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authorization;


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
    [Authorize(Roles = "Manager")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeDTO>>> GetEmployes()
    {
        var employes = _context.Employes.Select(x => new EmployeDTO(x));

        return await employes.ToListAsync();
    }

    // GET: api/employe/2
    [Authorize(Roles = "Manager")]
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeDTO>> GetEmploye(int id)
    {
        var employe = await _context.Employes.SingleOrDefaultAsync(t => t.Id == id);

        if (employe == null)
            return NotFound();

        return new EmployeDTO(employe);
    }

    // GET : api/employes/byTache/3
    [Authorize(Roles = "Manager")]
    [HttpGet("byTache/{idTache}")]
    public async Task<ActionResult<IEnumerable<EmployeDTO>>> GetEmployesByTacheId(int idTache)
    {
        // Premier étape: Récupérer toutes les assignations qui correspondent à l'idTache donné
        var assignations = await _context.Assignations
            .Where(a => a.IdTache == idTache) // Supposons que vous avez IdTache comme champ directement disponible
            .ToListAsync();

        if (assignations == null || assignations.Count == 0)
            return NotFound();

        // Deuxième étape: Extraire les ID employé et les déhasher si nécessaire (ou toute autre logique applicable en mémoire)
        var employeIds = assignations.Select(a => a.IdEmploye).ToList(); // Supposons que IdEmploye est directement disponible

        // Troisième étape: Utiliser les ID employé pour récupérer les employés correspondants
        var employes = await _context.Employes
            .Where(e => employeIds.Contains(e.Id))
            .Select(e => new EmployeDTO(e))
            .ToListAsync();

        if (employes == null || employes.Count == 0)
            return NotFound();

        return employes;
    }

    // GET: api/employe/byEmail/2
    [HttpGet("byEmail{email}")]
    public async Task<ActionResult<EmployeDTO>> GetEmployeByEmail(string email)
    {
        var employe = await _context.Employes.SingleOrDefaultAsync(e => e.Email == email);

        if (employe == null)
            return NotFound();

        return new EmployeDTO(employe);
    }


    // POST: api/employe
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<ActionResult<Employe>> PostEmploye(EmployeMdpDTO employeMdpDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!IsMotDePasseValid(employeMdpDTO.MotDePasse))
        {
            return BadRequest("Le mot de passe doit contenir au moins un caractère et un chiffre.");
        }

        Employe employe = new Employe(employeMdpDTO, _context);
        employe.SetMotDePasse(employeMdpDTO.MotDePasse);

        _context.Employes.Add(employe);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmploye), new { id = employe.Id }, new EmployeDTO(employe));
    }
    private bool IsMotDePasseValid(string motDePasse)
    {
        // Exemple de validation : Au moins un caractère et un chiffre
        return !string.IsNullOrWhiteSpace(motDePasse) && motDePasse.Any(char.IsLetter) && motDePasse.Any(char.IsDigit);
    }


    // DELETE: api/employes/2
    [Authorize(Roles = "Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeItem(int id)
    {
        var employe = await _context.Employes.FindAsync(id);

        if (employe == null)
            return NotFound();

        var assignations = _context.Assignations.Where(a => a.IdEmploye == id);
        _context.Assignations.RemoveRange(assignations);

        _context.Employes.Remove(employe);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // PUT: api/employe/2
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmploye(int id, EmployeDTO employeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

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
        existingEmploye.Statut = employeDTO.Statut.ToString();
        existingEmploye.Pole = employeDTO.Pole.ToString();

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

    private string HashId(int id)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(id.ToString()));
            return Convert.ToBase64String(hashedBytes);
        }
    }

}