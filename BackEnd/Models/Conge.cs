using System.Text;
public class Conge 
{
    public int Id { get; set; }
    public string HashedIdEmploye { get; set; }
    public double Duree { get; set; }
    public DateTime Date { get; set; }
    public string Motif { get; set; }
    public Conge() { }
    public Conge(CongeDTO congeDTO, BackendContext context)
    {
        Id = congeDTO.Id;
        HashedIdEmploye = congeDTO.HashedIdEmploye;
        Duree = congeDTO.Duree;
        Date = congeDTO.Date;
        Motif = congeDTO.Motif;
    }

    private string HashId(int id)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(id.ToString()));
            return Convert.ToBase64String(hashedBytes);
        }
    }
    public void SetId(int IdEmploye)
    {
        this.HashedIdEmploye = HashId(IdEmploye);
    }
}