using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

[ApiController]
[Route("api/assignation")]
public class AssignationController : ControllerBase
{
    private readonly BackendContext _context;
    public AssignationController(BackendContext context)
    {
        _context = context;
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