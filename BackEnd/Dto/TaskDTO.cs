using System.ComponentModel.DataAnnotations;
public class TaskDTO
{
    public int Id { get; set; }
    public double Duration { get; set; }

    [Required(ErrorMessage = "The description is required")]
    [StringLength(100, ErrorMessage = "The description must contains between 3 ans 100 characters", MinimumLength = 3)]
    public string Description { get; set; }
    public DateTime DueDate { get; set; }

    public TaskDTO() { }

    public TaskDTO(Task task)
    {
        Id = task.Id;
        Duration = task.Duration;
        Description = task.Description;
        DueDate = task.DueDate;
    }
}