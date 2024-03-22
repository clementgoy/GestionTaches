// ...other using statements...

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class AuthentificationController : ControllerBase
{
    private readonly BackendContext _context;
    private readonly IConfiguration _configuration;

    public AuthentificationController(BackendContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Connexion([FromBody] AuthentificationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var utilisateur = await _context.Employes
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (utilisateur == null || !BCrypt.Net.BCrypt.Verify(request.Password, utilisateur.MotDePasseHash))
        {
            return Unauthorized("Email ou mot de passe incorrect");
        }

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
        var tokenHandler = new JwtSecurityTokenHandler();
        var role = utilisateur.Statut; // Make sure 'Statut' is the property you use for roles

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, utilisateur.Email),
                new Claim(ClaimTypes.Role, role) // Assuming 'role' is a string. If not, convert it properly.
            }),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            Audience = _configuration["Jwt:Audience"],
            Issuer = _configuration["Jwt:Issuer"],
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }
}
