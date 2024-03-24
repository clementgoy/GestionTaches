using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Mail;

[ApiController]
[Route("api/forgetpassword")]
public class ForgetPasswordController : ControllerBase
{
    private readonly BackendContext _context;

    // Constructor initializes the database context
    public ForgetPasswordController(BackendContext context)
    {
        _context = context;
    }

    [HttpPost("request-reset")]
    public async Task<IActionResult> RequestResetPassword(RequestReset model)
    {
        // Validates the request model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Attempts to find the user by email
        var user = await _context.Employees.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user == null) return BadRequest("Employee not found");

        // Generates a unique reset token
        var token = GenerateToken();

        user.ResetToken = token;
        user.ResetTokenExpires = DateTime.UtcNow.AddHours(1);

        await _context.SaveChangesAsync();

        // Sends a reset email with the generated token
        var resetLink = $"http://localhost:8080/reset-password?token={token}";
        SendResetEmail(user.Email, resetLink);

        return Ok("Reset email sent");
    }

    // Method for sending the reset email
    private void SendResetEmail(string email, string link)
    {
        // SMTP client configuration
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("gesttaches", "ouck rktq tgin oled"),
            EnableSsl = true,
        };

        // Constructs the email message
        var mailMessage = new MailMessage
        {
            From = new MailAddress("noreply@example.com"),
            Subject = "Password reset",
            Body = $"Please click on the following link to reset your password : {link}",
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

        // Find the user by reset token and ensure the token hasn't expired
        var user = _context.Employees.FirstOrDefault(u => u.ResetToken == model.Token && u.ResetTokenExpires > DateTime.UtcNow);
        if (user == null) return BadRequest("Invalid or expired token");

        // Replace the password value 
        user.SetPassword(model.NewPassword);
        user.ResetToken = null;
        user.ResetTokenExpires = null;

        await _context.SaveChangesAsync();

        return Ok("Password updated");
    }

    public static string GenerateToken()
    {
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            byte[] tokenData = new byte[32]; // Create 256 bits token
            rng.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }
    }

}
