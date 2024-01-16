public class AssignationDTO
{
    public int IdEmploye { get; set; }
    public int IdTache { get; set; }
    public string Message { get; set; }

    public AssignationDTO() { }

    public AssignationDTO(Assignation assignation)
    {
        IdEmploye = assignation.IdEmploye;
        IdTache = assignation.IdTache;
        Message = assignation.Message;
    }
}