public class ResetPassword
{
    public string Token { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public ResetPassword() { }
    public ResetPassword(ResetPasswordDTO resetPasswordDTO, BackendContext context)
    {
        Token = resetPasswordDTO.Token;
        Password = resetPasswordDTO.Password;
        ConfirmPassword = resetPasswordDTO.ConfirmPassword;
    }
}
