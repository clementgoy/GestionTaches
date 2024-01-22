public class ResetPasswordDTO
{
    public string Token { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public ResetPasswordDTO() { }
    public ResetPasswordDTO(ResetPassword resetPassword, BackendContext context)
    {
        Token = resetPassword.Token;
        Password = resetPassword.Password;
        ConfirmPassword = resetPassword.ConfirmPassword;
    }
}
