using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


[ApiController]
[Route("api/auth")]
public class AuthentificationController : ControllerBase
{
    private readonly BackendContext _context;

    public AuthentificationController(BackendContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Connexion([FromBody] JObject donneesRequête)
    {
        string courriel = donneesRequête.GetValue("courriel")?.ToString();
        string motDePasse = donneesRequête.GetValue("motDePasse")?.ToString();

        // Utilisez 'await' ici pour attendre le résultat de la méthode asynchrone
        if (await InformationsConnexionValides(courriel, motDePasse))
        {
            // Authentification réussie
            return Ok(new { message = "Authentification réussie" });
        }
        else
        {
            // Authentification échouée
            return Unauthorized(new { message = "Courriel ou mot de passe incorrect" });
        }
    }

    private async Task<bool> InformationsConnexionValides(string courriel, string motDePasse)
    {
        EmployeController employeController = new EmployeController(_context);

        // Appel de la méthode GetEmployeByEmail de manière asynchrone
        var employeResult = await employeController.GetEmployeByEmail(courriel);

        if (employeResult != null && employeResult.Value != null)
        {
            EmployeDTO employe = employeResult.Value;
            return BCrypt.Net.BCrypt.Verify(motDePasse, employe.MotDePasseHash);
        }

        return false;
    }
}