using System.ComponentModel.DataAnnotations;
public class EmployeePasswordDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [StringLength(100, ErrorMessage = "The name must conatains between 3 and 50 characters", MinimumLength = 3)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The first name is required")]
    [StringLength(100, ErrorMessage = "The first name must conatains between 3 and 50 characters", MinimumLength = 3)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The email is required")]
    [EmailAddress(ErrorMessage = "the email format is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The password is required")]
    [StringLength(100, ErrorMessage = "The password must conatains between 3 and 50 characters", MinimumLength = 3)]
    public string Password { get; set; }

    [Required(ErrorMessage = "The status is required")]
    public StatusEmployee Status { get; set; }

    [Required(ErrorMessage = "The pole is required")]
    public PoleEmployee Pole { get; set; }

    public EmployeePasswordDTO() { }

    public EmployeePasswordDTO(Employee employee)
    {
        Id = employee.Id;
        Name = employee.Name;
        FirstName = employee.FirstName;
        Email = employee.Email;
        SetStatus(employee.Status);
        SetPole(employee.Pole);
    }

    public void SetStatus(string status)
    {
        if (Enum.TryParse<StatusEmployee>(status, out var parsedStatus))
        {
            Status = parsedStatus;
        }
        else
        {
            // Gérer le cas où la conversion échoue
            throw new ArgumentException("Invalid status");
        }
    }
    public void SetPole(string pole)
    {
        if (Enum.TryParse<PoleEmployee>(pole, out var parsedPole))
        {
            Pole = parsedPole;
        }
        else
        {
            // Gérer le cas où la conversion échoue
            throw new ArgumentException("Invalid pole");
        }
    }
}