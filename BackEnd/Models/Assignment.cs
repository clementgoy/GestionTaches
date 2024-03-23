public class Assignment
{
    public int Id { get; set; }
    public int IdEmployee { get; set; }
    public int IdTask { get; set; }
    public string Message { get; set; } = null!;

    public Assignment() { }

    public Assignment(AssignmentDTO assignmentDTO, BackendContext context)
    {
        IdEmployee = assignmentDTO.IdEmployee;
        IdTask = assignmentDTO.IdTask;
        Message = assignmentDTO.Message;
    }

    public void Update(AssignmentDTO AssignmentDTO)
    {
        IdEmployee = AssignmentDTO.IdEmployee;
        IdTask = AssignmentDTO.IdTask;
        Message = AssignmentDTO.Message;
    }
}
