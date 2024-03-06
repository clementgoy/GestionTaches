using System.ComponentModel.DataAnnotations;
public class ResetPasswordDTO
{
    [Required]
    public string Token { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; }

    public ResetPasswordDTO() { }

    public ResetPasswordDTO(ResetPassword resetPassword)
    {
        Token = resetPassword.Token;
        NewPassword = resetPassword.NewPassword;
    }
}