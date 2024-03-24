using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authorization;


[ApiController]
[Route("api/employes")]
public class EmployeController : ControllerBase
{
    private readonly BackendContext _context;

    // Injects the database context
    public EmployeController(BackendContext context)
    {
        _context = context;
    }

    // GET: api/employes
    [Authorize(Roles = "Manager")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
    {
        // Projects each Employee entity to an EmployeeDTO and returns the list
        var employees = _context.Employees.Select(x => new EmployeeDTO(x));

        return await employees.ToListAsync();
    }

    // GET: api/employe/2
    [Authorize(Roles = "Manager")]
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
    {
        // Finds the employee by ID, returns 404 Not Found if not found
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
        // Fetches all assignments that match the given task ID
        var assignment = await _context.Assignments
            .Where(a => a.IdTask == idTask)
            .ToListAsync();

        if (assignment == null || assignment.Count == 0)
            return NotFound();

        // Extracts employee IDs from the assignments and fetches corresponding employees
        var employeeIds = assignment.Select(a => a.IdEmployee).ToList();

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
        // Finds the employee by email, returns 404 Not Found if not found
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
        // Validates the request model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validates the provided password
        if (!IsPasswordValid(employeePasswordDTO.Password))
        {
            return BadRequest("Le mot de passe doit contenir au moins un caractère et un chiffre.");
        }

        // Creates a new Employee entity from the provided DTO and sets its password
        Employee employee = new Employee(employeePasswordDTO, _context);
        employee.SetPassword(employeePasswordDTO.Password);

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        // Returns the newly created employee
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, new EmployeeDTO(employee));
    }

    // Validates the provided password
    private bool IsPasswordValid(string Password)
    {
        return !string.IsNullOrWhiteSpace(Password) && Password.Any(char.IsLetter) && Password.Any(char.IsDigit);
    }


    // DELETE: api/employes/2
    [Authorize(Roles = "Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeItem(int id)
    {
        // Finds the employee by ID, returns 404 Not Found if not found
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return NotFound();

        // Removes any assignments associated with the employee and the employee itself, then saves changes
        var assignment = _context.Assignments.Where(a => a.IdEmployee == id);
        _context.Assignments.RemoveRange(assignment);

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent(); // Indicates successful deletion with no content in the response
    }


    // PUT: api/employe/2
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employeDTO)
    {
        // Validates the request model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Ensures the ID from the URL matches the ID in the DTO
        if (id != employeDTO.Id)
            return BadRequest();

        // Attempts to find the existing employee by ID
        Employee existingEmployee = await _context.Employees.FindAsync(id);

        if (existingEmployee == null)
            return NotFound();

        // Updates the existing employee's properties with the values from the DTO
        existingEmployee.Name = employeDTO.Name;
        existingEmployee.FirstName = employeDTO.FirstName;
        existingEmployee.Email = employeDTO.Email;
        existingEmployee.Status = employeDTO.Status.ToString();
        existingEmployee.Pole = employeDTO.Pole.ToString();

        // Marks the entity as modified to ensure the changes are updated in the database
        _context.Entry(existingEmployee).State = EntityState.Modified;

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

        return NoContent(); // Indicates successful update with no content in the response
    }

    // Generates a base64-encoded hash of the given integer ID using SHA256
    private string HashId(int id)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            // Converts the ID to a byte array and computes its hash
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(id.ToString()));

            //Converts the hash to a base64 string and returns it
            return Convert.ToBase64String(hashedBytes);
        }
    }

}