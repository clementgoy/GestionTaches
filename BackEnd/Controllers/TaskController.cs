using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


[ApiController]
[Route("api/Tasks")]
public class taskController : ControllerBase
{
    private readonly BackendContext _context;

    // Initializes the controller with the database context
    public taskController(BackendContext context)
    {
        _context = context;
    }


    // GET: api/tasks
    [Authorize(Roles = "Manager")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasks()
    {
        // Projects each Task entity to a TaskDTO and returns the list
        var tasks = _context.Tasks.Select(x => new TaskDTO(x));
        return await tasks.ToListAsync();
    }


    // GET: api/task/2
    [Authorize(Roles = "Manager")]
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDTO>> GetTask(int id)
    {
        // Attempts to find the task by ID, returns 404 Not Found if it doesn't exist
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
        // Fetches assignments for the specified employee and then retrieves the corresponding tasks
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
    public async Task<ActionResult<Task>> PostTask(TaskDTO taskDTO)
    {
        // Creates a new Task entity from the DTO and adds it to the database
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
        // Finds the task by ID, returns 404 Not Found if it doesn't exist
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
            return NotFound();

        // Deletes the task and any related assignments, then saves changes to the database
        var assignments = _context.Assignments.Where(a => a.IdTask == id);
        _context.Assignments.RemoveRange(assignments);

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent(); // Indicates successful deletion with no content in the response.
    }


    // PUT: api/task/2
    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTask(int id, TaskDTO taskDTO)
    {
        // Validates the ID match between the URL and the DTO
        if (id != taskDTO.Id)
        {
            return BadRequest("The task id does not match the one in the URL");
        }

        // Attempts to find the existing task
        var existingtask = await _context.Tasks.FindAsync(id);

        if (existingtask == null)
        {
            return NotFound("No task found with this id");
        }

        existingtask.Duration = taskDTO.Duration;
        existingtask.Description = taskDTO.Description;
        existingtask.DueDate = taskDTO.DueDate;

        // Marks the task entity as modified and attempts to save changes
        _context.Entry(existingtask).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Tasks.Any(e => e.Id == id)) // Checks if the task still exists
            {
                return NotFound();
            }
            else
            {
                throw; // Re-throws exception for unhandled cases
            }
        }

        return NoContent(); // Indicates successful update with no content in the response
    }

}