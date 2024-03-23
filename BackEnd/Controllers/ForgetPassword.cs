using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;

[ApiController]
[Route("api/forgetpassword")]
public class ForgetPasswordController : ControllerBase
{
    private readonly BackendContext _context;
    public ForgetPasswordController(BackendContext context)
    {
        _context = context;
    }

    [HttpPost("request-reset")]
    public async Task<IActionResult> RequestResetPassword(RequestReset model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Employees.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user == null) return BadRequest("Utilisateur non trouvé");

        // Générer un jeton unique
        var token = Guid.NewGuid().ToString();
        user.ResetToken = token;
        user.ResetTokenExpires = DateTime.UtcNow.AddHours(1);

        await _context.SaveChangesAsync();

        // Envoyer l'email
        var resetLink = $"http://localhost:8080/reset-password?token={token}";
        SendResetEmail(user.Email, resetLink);

        return Ok("Email de réinitialisation envoyé.");
    }

    private void SendResetEmail(string email, string link)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("gesttaches", "ouck rktq tgin oled"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("noreply@example.com"),
            Subject = "Réinitialisation de votre mot de passe",
            Body = $"Veuillez cliquer sur le lien suivant pour réinitialiser votre mot de passe: {link}",
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        smtpClient.Send(mailMessage);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPassword model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _context.Employees.FirstOrDefault(u => u.ResetToken == model.Token && u.ResetTokenExpires > DateTime.UtcNow);
        if (user == null) return BadRequest("Invalid or expired token");

        user.SetPassword(model.NewPassword);
        user.ResetToken = null;
        user.ResetTokenExpires = null;

        await _context.SaveChangesAsync();

        return Ok("Password updated");
    }

}