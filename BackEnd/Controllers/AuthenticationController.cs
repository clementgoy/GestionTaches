using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


[Route("api/[controller]")]
[ApiController]
public class AuthentificationController : ControllerBase
{
    private readonly BackendContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthentificationController> _logger;

    public AuthentificationController(BackendContext context, IConfiguration configuration, ILogger<AuthentificationController> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Attempt to retrieve the user
        var user = await _context.Employees.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
        {
            _logger.LogWarning($"Failed login attempt for {request.Email}");
            return Unauthorized("Incorrect email or password");
        }

        _logger.LogInformation($"User {request.Email} successfully logged in.");

        // Generates a JWT token for the authenticated user
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
        var tokenHandler = new JwtSecurityTokenHandler();
        var role = user.Status;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, role)
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
