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

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPassword model)
    {
        try
        {
            // Vérifier si le token est valide
            var employe = _context.Employes.FirstOrDefault(e => e.ReinitialiserMDPJeton == model.Token);

            if (employe == null || TokenHasExpired(employe.ReinitialiserMDPJetonExpiration))
            {
                // Jeton non trouvé ou expiré
                return BadRequest(new { message = "Token invalide ou expiré." });
            }

            // Vérifier si les mots de passe correspondent
            if (model.Password != model.ConfirmPassword)
            {
                // Mots de passe ne correspondent pas
                return BadRequest(new { message = "Les mots de passe ne correspondent pas." });
            }

            // Réinitialiser le mot de passe en utilisant la méthode SetMotDePasse qui prend en charge le hachage
            employe.SetMotDePasse(model.Password);

            // Effacer le token de réinitialisation
            employe.ReinitialiserMDPJeton = null;
            employe.ReinitialiserMDPJetonExpiration = null;

            // Supprimer le mot de passe du modèle
            model.Password = null;
            model.ConfirmPassword = null;

            await _context.SaveChangesAsync();

            // Envoyer l'e-mail
            SendPasswordResetEmail(employe.Email);

            return Ok(new { message = "Réinitialisation de mot de passe réussie." });
        }
        catch (Exception ex)
        {
            // Gérer toute exception
            Console.WriteLine($"Erreur lors de la réinitialisation de mot de passe : {ex.Message}");
            return StatusCode(500, new { message = "Erreur interne du serveur lors de la réinitialisation de mot de passe." });
        }
    }


    private bool TokenHasExpired(DateTime? expirationDate)
    {
        return expirationDate == null || DateTime.UtcNow > expirationDate.Value;
    }

    private void SendPasswordResetEmail(string userEmail)
    {
        try
        {
            // Configurer les paramètres SMTP
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("cgoy001@ensc.fr", "monmotdepasse"),
                EnableSsl = true,
            };

            // Construire l'e-mail
            var mailMessage = new MailMessage
            {
                From = new MailAddress("cgoy001@ensc.fr"),
                Subject = "Réinitialisation de mot de passe",
                Body = "Votre mot de passe a été réinitialisé avec succès.",
                IsBodyHtml = false,
            };

            mailMessage.To.Add(userEmail);

            // Envoyer l'e-mail
            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            // Gérer les erreurs d'envoi d'e-mail, par exemple, journaliser l'erreur
            Console.WriteLine($"Erreur lors de l'envoi de l'e-mail : {ex.Message}");
        }
    }

}