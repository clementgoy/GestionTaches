using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/employe")]
public class EmployeController : ControllerBase
{
    private readonly BackendContext _context;
    public EmployeController(BackendContext context)
    {
        _context = context;
    }

    // GET: api/employes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employe>>> GetEmployes()
    {
        var employes = await _context.Employes.ToListAsync();

        foreach (Employe emp in employes)
        {
            var assignations = await _context.Assignations.Where(d => d.IdEmploye == emp.Id).ToListAsync();
            var travails = new List<int>();

            foreach (var assignation in assignations)
            {
                travails.Add(assignation.IdTache);
            }

            emp.Travail = travails;
        }

        return employes;
    }

}