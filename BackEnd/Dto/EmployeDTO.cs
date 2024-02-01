public class EmployeDTO
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string MotDePasse { get; set; }
    public string MotDePasseHash { get; set; }
    public string ReinitialiserMDPJeton { get; set; }
    public DateTime? ReinitialiserMDPJetonExpiration { get; set; }
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
        ReinitialiserMDPJeton = employe.ReinitialiserMDPJeton;
        ReinitialiserMDPJetonExpiration = employe.ReinitialiserMDPJetonExpiration;
        Statut = employe.Statut.ToString();
        Pole = employe.Pole.ToString();
    }
}