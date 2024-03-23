public class Task
{
    public int Id { get; set; }
    public double Duration { get; set; }
    public string Description { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public Task() { }
    public Task(TaskDTO taskDTO, BackendContext context)
    {
        Id = taskDTO.Id;
        Duration = taskDTO.Duration;
        Description = taskDTO.Description;
        DueDate = taskDTO.DueDate;
    }

}