public class Employe
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string MotDePasseHash { get; set; }
    internal string? JetonConnection { get; set; }
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public string Statut { get; set; }
    public string Pole { get; set; }
    public Employe() { }
    public Employe(EmployeMdpDTO employeMdpDTO, BackendContext context)
    {
        Id = employeMdpDTO.Id;
        Nom = employeMdpDTO.Nom;
        Prenom = employeMdpDTO.Prenom;
        Email = employeMdpDTO.Email;
        SetMotDePasse(employeMdpDTO.MotDePasse);
        Statut = employeMdpDTO.Statut.ToString();
        Pole = employeMdpDTO.Pole.ToString();
    }

    public void SetMotDePasse(string motDePasse)
    {
        if (IsMotDePasseValid(motDePasse))
        {
            // Hacher le mot de passe avant de le stocker
            this.MotDePasseHash = BCrypt.Net.BCrypt.HashPassword(motDePasse);

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
}