public class RequestReset
{
    public string Email { get; set; }

    public RequestReset() { }
    public RequestReset(RequestResetDTO requestResetDTO, BackendContext context)
    {
        Email = requestResetDTO.Email;
    }
}