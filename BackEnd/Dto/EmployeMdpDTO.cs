using System.ComponentModel.DataAnnotations;
public class EmployeMdpDTO
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

    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    [StringLength(100, ErrorMessage = "Le mot de passe doit contenir entre 3 et 50 caractères", MinimumLength = 3)]
    public string MotDePasse { get; set; }

    [Required(ErrorMessage = "Le statut est obligatoire")]
    public StatutEmploye Statut { get; set; }

    [Required(ErrorMessage = "Le pole est obligatoire")]
    public PoleEntreprise Pole { get; set; }

    public EmployeMdpDTO() { }

    public EmployeMdpDTO(Employe employe)
    {
        Id = employe.Id;
        Nom = employe.Nom;
        Prenom = employe.Prenom;
        Email = employe.Email;
        SetStatut(employe.Statut);
        SetPole(employe.Pole);
    }

    public void SetStatut(string statut)
    {
        if (Enum.TryParse<StatutEmploye>(statut, out var parsedStatut))
        {
            Statut = parsedStatut;
        }
        else
        {
            // Gérer le cas où la conversion échoue
            throw new ArgumentException("Statut non valide");
        }
    }
    public void SetPole(string pole)
    {
        if (Enum.TryParse<PoleEntreprise>(pole, out var parsedPole))
        {
            Pole = parsedPole;
        }
        else
        {
            // Gérer le cas où la conversion échoue
            throw new ArgumentException("Pôle non valide");
        }
    }
}
