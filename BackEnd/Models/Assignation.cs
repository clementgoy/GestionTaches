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

    public void Update(AssignationDTO assignationDTO)
    {

        HashedIdEmploye = HashId(assignationDTO.IdEmploye);
        HashedIdTache = HashId(assignationDTO.IdTache);
        Message = assignationDTO.Message;

        assignationDTO.IdTache = 0;
        assignationDTO.IdEmploye = 0;
        assignationDTO.Message = null!;
    }
    private string HashId(int id)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(id.ToString()));
            return Convert.ToBase64String(hashedBytes);
        }
    }
    public void SetIds(int IdTache, int IdEmploye)
    {
        this.HashedIdTache = HashId(IdTache);
        this.HashedIdEmploye = HashId(IdEmploye);
    }
}
