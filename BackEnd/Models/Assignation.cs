using System.Text;

public class Assignation
{
    public int Id { get; set; }
    public string HashedIdEmploye { get; set; }
    public string HashedIdTache { get; set; }
    public string Message { get; set; } = null!;

    public Assignation() { }

    public Assignation(AssignationDTO assignationDTO, BackendContext context)
    {
        HashedIdEmploye = HashId(assignationDTO.IdEmploye);
        HashedIdTache = HashId(assignationDTO.IdTache);

        Message = assignationDTO.Message;
    }

    private string HashId(int id)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(id.ToString()));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
