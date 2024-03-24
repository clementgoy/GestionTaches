using System.ComponentModel.DataAnnotations;
public class EmployeeDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [StringLength(100, ErrorMessage = "The name must conatains between 3 and 50 characters", MinimumLength = 3)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The first name is required")]
    [StringLength(100, ErrorMessage = "The first name must conatains between 3 and 50 characters", MinimumLength = 3)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The email is required")]
    [EmailAddress(ErrorMessage = "The email format is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The status is required")]
    public StatusEmployee Status { get; set; }

    [Required(ErrorMessage = "The pole is required")]
    public PoleEmployee Pole { get; set; }

    public EmployeeDTO() { }

    public EmployeeDTO(Employee Employee)
    {
        Id = Employee.Id;
        Name = Employee.Name;
        FirstName = Employee.FirstName;
        Email = Employee.Email;
        SetStatus(Employee.Status);
        SetPole(Employee.Pole);
    }

    public void SetStatus(string status)
    {
        if (Enum.TryParse<StatusEmployee>(status, out var parsedStatus))
        {
            Status = parsedStatus;
        }
        else
        {
            // Handle the case where the conversion fails
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
            // Handle the case where the conversion fails
            throw new ArgumentException("Invalid pole");
        }
    }
}

public enum StatusEmployee
{
    Manager,
    TeamMember
}
public enum PoleEmployee
{
    Logistics,
    Development,
    Design,
    Production
}