public enum StatutEmploye
{
    Manager,
    MembreEquipe
}
public enum PoleEntreprise
{
    Logistique,
    Developpement,
    Design,
    Production
}

public class Employe
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    private string MotDePasse { get; set; }
    public List<int> Travail { get; set; }
    public StatutEmploye Statut { get; set; }
    public PoleEntreprise Pole { get; set; }
    public Employe() { }
    public Employe(EmployeDTO employeDTO, BackendContext context)
    {
        Nom = employeDTO.Nom;
        Prenom = employeDTO.Prenom;
        Email = employeDTO.Email;
        Travail = employeDTO.Travail;
        Statut = employeDTO.Statut;
        Pole = employeDTO.Pole;
    }

}