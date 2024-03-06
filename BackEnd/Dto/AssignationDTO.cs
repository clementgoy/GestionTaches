using System.ComponentModel.DataAnnotations;
public class AssignationDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire")]
    public int IdEmploye { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire")]
    public int IdTache { get; set; }

    [StringLength(100, ErrorMessage = "Le nom doit contenir entre 3 et 50 caractères", MinimumLength = 3)]
    public string Message { get; set; }

    public AssignationDTO() { }

    public AssignationDTO(Assignation assignation)
    {
        Id = assignation.Id;
        Message = assignation.Message;
    }
}