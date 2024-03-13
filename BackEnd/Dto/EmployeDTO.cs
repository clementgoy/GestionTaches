using System.ComponentModel.DataAnnotations;
public class EmployeDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire")]
    [StringLength(100, ErrorMessage = "Le nom doit contenir entre 3 et 50 caractères", MinimumLength = 3)]
    public string Nom { get; set; }

    [Required(ErrorMessage = "Le prenom est obligatoire")]
    [StringLength(100, ErrorMessage = "Le prenom doit contenir entre 3 et 50 caractères", MinimumLength = 3)]
    public string Prenom { get; set; }

    [Required(ErrorMessage = "L'email est obligatoire")]
    [EmailAddress(ErrorMessage = "Le format de l'email n'est pas valide")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Le statut est obligatoire")]
    [StringLength(100, ErrorMessage = "Le statut doit contenir entre 3 et 50 caractères", MinimumLength = 3)]
    public string Statut { get; set; }

    [Required(ErrorMessage = "Le pole est obligatoire")]
    [StringLength(100, ErrorMessage = "Le pole doit contenir entre 3 et 50 caractères", MinimumLength = 3)]
    public string Pole { get; set; }

    public EmployeDTO() { }

    public EmployeDTO(Employe employe)
    {
        Id = employe.Id;
        Nom = employe.Nom;
        Prenom = employe.Prenom;
        Email = employe.Email;
        Statut = employe.Statut.ToString();
        Pole = employe.Pole.ToString();
    }
}