public class EmployeDTO
{
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public List<int> Travail { get; set; }
    public StatutEmploye Statut { get; set; }
    public PoleEntreprise Pole { get; set; }

    public EmployeDTO() { }

    public EmployeDTO(Employe employe)
    {
        Nom = employe.Nom;
        Prenom = employe.Prenom;
        Email = employe.Email;
        Travail = employe.Travail;
        Statut = employe.Statut;
        Pole = employe.Pole;
    }
}