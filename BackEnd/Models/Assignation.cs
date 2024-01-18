public class Assignation
{
    public int Id { get; set; }
    public int IdEmploye { get; set; }
    public int IdTache { get; set; }
    public string Message { get; set; } = null!;
    public Assignation() { }
    public Assignation(AssignationDTO assignationDTO, BackendContext context)
    {
        Id = assignationDTO.Id;
        IdEmploye = assignationDTO.IdEmploye;
        IdTache = assignationDTO.IdTache;
        Message = assignationDTO.Message;
    }
}