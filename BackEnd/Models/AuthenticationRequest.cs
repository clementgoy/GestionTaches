using System.ComponentModel.DataAnnotations;
public class AuthenticationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Password { get; set; }
}
