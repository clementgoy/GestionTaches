using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


[ApiController]
[Route("api/Tasks")]
public class taskController : ControllerBase
{
    private readonly BackendContext _context;
    public taskController(BackendContext context)
    {
        _context = context;
    }


    // GET: api/tasks
    [Authorize(Roles = "Manager")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasks()
    {
        var Tasks = _context.Tasks.Select(x => new TaskDTO(x));
        return await Tasks.ToListAsync();
    }


    // GET: api/task/2
    [Authorize(Roles = "Manager")]
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDTO>> GetTask(int id)
    {
        var task = await _context.Tasks.SingleOrDefaultAsync(t => t.Id == id);

        if (task == null)
            return NotFound();

        return new TaskDTO(task);
    }


    // GET : api/tasks/byEmployee/3
    [Authorize]
    [HttpGet("byEmployee/{idEmployee}")]
    public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasksByEmployeId(int idEmployee)
    {

        var assignments = await _context.Assignments
            .Where(a => a.IdEmployee == idEmployee)
            .ToListAsync();

        if (assignments == null || assignments.Count == 0)
            return NotFound();

        var taskIds = assignments.Select(a => a.IdTask).ToList();

        var tasks = await _context.Tasks
            .Where(t => taskIds.Contains(t.Id))
            .ToListAsync();

        if (tasks == null || tasks.Count == 0)
            return NotFound();

        return tasks.Select(t => new TaskDTO(t)).ToList();
    }


    // POST: api/task
    [Authorize(Roles = "Manager")]
    [HttpPost]
    public async Task<ActionResult<Task>> Posttask(TaskDTO taskDTO)
    {
        Task task = new Task(taskDTO, _context);

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, new TaskDTO(task));
    }


    // DELETE: api/tasks/2
    [Authorize(Roles = "Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
            return NotFound();

        var assignments = _context.Assignments.Where(a => a.IdTask == id);
        _context.Assignments.RemoveRange(assignments);

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    // PUT: api/task/2
    [Authorize(Roles = "Manager")]
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTask(int id, TaskDTO taskDTO)
    {
        if (id != taskDTO.Id)
        {
            return BadRequest("L'ID de la tâche ne correspond pas à celui dans l'URL.");
        }

        var existingtask = await _context.Tasks.FindAsync(id);

        if (existingtask == null)
        {
            return NotFound("No task found with this id");
        }

        existingtask.Duration = taskDTO.Duration;
        existingtask.Description = taskDTO.Description;
        existingtask.DueDate = taskDTO.DueDate;

        _context.Entry(existingtask).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Tasks.Any(e => e.Id == id))
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

}