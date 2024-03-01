public class EmployeDTO
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string MotDePasse { get; set; }
    public string MotDePasseHash { get; set; }
    public string ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public string Statut { get; set; }
    public string Pole { get; set; }

    public EmployeDTO() { }

    public EmployeDTO(Employe employe)
    {
        Id = employe.Id;
        Nom = employe.Nom;
        Prenom = employe.Prenom;
        Email = employe.Email;
        MotDePasseHash = employe.MotDePasseHash;
        ResetToken = employe.ResetToken;
        ResetTokenExpires = employe.ResetTokenExpires;
        Statut = employe.Statut.ToString();
        Pole = employe.Pole.ToString();
    }
}