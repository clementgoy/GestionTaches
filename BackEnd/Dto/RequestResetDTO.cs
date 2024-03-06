using System.ComponentModel.DataAnnotations;
public class RequestResetDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public RequestResetDTO() { }

    public RequestResetDTO(RequestReset requestReset)
    {
        Email = requestReset.Email;
    }
}