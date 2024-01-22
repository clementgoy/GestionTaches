public class AssignationDTO
{
    public int Id { get; set; }
    public int IdEmploye { get; set; } 
    public int IdTache { get; set; }
    public string Message { get; set; }

    public AssignationDTO() { }

    public AssignationDTO(Assignation assignation)
    {
        Id = assignation.Id;
        Message = assignation.Message;
    }
}