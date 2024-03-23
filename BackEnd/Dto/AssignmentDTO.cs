using System.ComponentModel.DataAnnotations;
public class AssignmentDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The employee id is required")]
    public int IdEmployee { get; set; }

    [Required(ErrorMessage = "The task id is required")]
    public int IdTask { get; set; }

    [StringLength(100, ErrorMessage = "The message must contains between 3 and 100 characters", MinimumLength = 3)]
    public string Message { get; set; }

    public AssignmentDTO() { }

    public AssignmentDTO(Assignment assignment)
    {
        Id = assignment.Id;
        IdEmployee = assignment.IdEmployee;
        IdTask = assignment.IdTask;
        Message = assignment.Message;
    }
}