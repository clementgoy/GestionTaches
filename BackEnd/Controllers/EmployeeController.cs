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
    public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
    {
        var employees = _context.Employees.Select(x => new EmployeeDTO(x));

        return await employees.ToListAsync();
    }

    // GET: api/employe/2
    [Authorize(Roles = "Manager")]
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
    {
        var employee = await _context.Employees.SingleOrDefaultAsync(t => t.Id == id);

        if (employee == null)
            return NotFound();

        return new EmployeeDTO(employee);
    }

    // GET : api/employes/byTache/3
    [Authorize(Roles = "Manager")]
    [HttpGet("byTache/{idTask}")]
    public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesByTacheId(int idTask)
    {
        // Récupérer toutes les assignment qui correspondent à l'idTask donné
        var assignment = await _context.Assignments
            .Where(a => a.IdTask == idTask) // Supposons que vous avez idTask comme champ directement disponible
            .ToListAsync();

        if (assignment == null || assignment.Count == 0)
            return NotFound();

        var employeeIds = assignment.Select(a => a.IdEmployee).ToList(); 

        // Utiliser les ID employé pour récupérer les employés correspondants
        var employees = await _context.Employees
            .Where(e => employeeIds.Contains(e.Id))
            .Select(e => new EmployeeDTO(e))
            .ToListAsync();

        if (employees == null || employees.Count == 0)
            return NotFound();

        return employees;
    }

    // GET: api/employe/byEmail/2
    [HttpGet("byEmail{email}")]
    public async Task<ActionResult<EmployeeDTO>> GetEmployeeByEmail(string email)
    {
        var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Email == email);

        if (employee == null)
            return NotFound();

        return new EmployeeDTO(employee);
    }


    // POST: api/employe
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(EmployeePasswordDTO employeePasswordDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!IsPasswordValid(employeePasswordDTO.Password))
        {
            return BadRequest("Le mot de passe doit contenir au moins un caractère et un chiffre.");
        }

        Employee employee = new Employee(employeePasswordDTO, _context);
        employee.SetPassword(employeePasswordDTO.Password);

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, new EmployeeDTO(employee));
    }
    private bool IsPasswordValid(string Password)
    {
        // Exemple de validation : Au moins un caractère et un chiffre
        return !string.IsNullOrWhiteSpace(Password) && Password.Any(char.IsLetter) && Password.Any(char.IsDigit);
    }


    // DELETE: api/employes/2
    [Authorize(Roles = "Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeItem(int id)
    {
        var employe = await _context.Employees.FindAsync(id);

        if (employe == null)
            return NotFound();

        var assignment = _context.Assignments.Where(a => a.IdEmployee == id);
        _context.Assignments.RemoveRange(assignment);

        _context.Employees.Remove(employe);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // PUT: api/employe/2
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != employeDTO.Id)
            return BadRequest();

        // Récupérer l'employé existant
        Employee existingEmploye = await _context.Employees.FindAsync(id);

        if (existingEmploye == null)
            return NotFound();

        // Update of the properties without changing the password
        existingEmploye.Name = employeDTO.Name;
        existingEmploye.FirstName = employeDTO.FirstName;
        existingEmploye.Email = employeDTO.Email;
        existingEmploye.Status = employeDTO.Status.ToString();
        existingEmploye.Pole = employeDTO.Pole.ToString();

        // Mettre à jour l'employé dans le contexte et sauvegarder les modifications
        _context.Entry(existingEmploye).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Employees.Any(m => m.Id == id))
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