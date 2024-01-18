public class Employe
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    internal string MotDePasse { get; set; } = null!;
    public StatutEmploye Statut { get; set; }
    public PoleEntreprise Pole { get; set; }
    public Employe() { }
    public Employe(EmployeDTO employeDTO, BackendContext context)
    {
        Id = employeDTO.Id;
        Nom = employeDTO.Nom;
        Prenom = employeDTO.Prenom;
        Email = employeDTO.Email;
        SetMotDePasse(employeDTO.MotDePasse);
        SetStatut(employeDTO.Statut);
        SetPole(employeDTO.Pole);
    }

    public void SetMotDePasse(string motDePasse)
    {
        if (IsMotDePasseValid(motDePasse))
        {
            MotDePasse = motDePasse;
        }
        else
        {
            // Gérer le cas où la validation échoue
            throw new ArgumentException("Le mot de passe doit contenir au moins un caractère et un chiffre.");
        }
    }

    private bool IsMotDePasseValid(string motDePasse)
    {
        // Exemple de validation : Au moins un caractère et un chiffre
        return !string.IsNullOrWhiteSpace(motDePasse) && motDePasse.Any(char.IsLetter) && motDePasse.Any(char.IsDigit);
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

