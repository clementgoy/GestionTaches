public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public string Status { get; set; }
    public string Pole { get; set; }
    public Employee() { }
    public Employee(EmployeePasswordDTO employeePasswordDTO, BackendContext context)
    {
        Id = employeePasswordDTO.Id;
        Name = employeePasswordDTO.Name;
        FirstName = employeePasswordDTO.FirstName;
        Email = employeePasswordDTO.Email;
        Status = employeePasswordDTO.Status.ToString();
        Pole = employeePasswordDTO.Pole.ToString();
    }

    public void SetPassword(string password)
    {
        if (IsPasswordValid(password))
        {
            // Hacher le mot de passe avant de le stocker
            this.HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        }
        else
        {
            // Gérer le cas où la validation échoue
            throw new ArgumentException("The password must contain at least one character and one number.");
        }
    }

    private bool IsPasswordValid(string password)
    {
        // Exemple de validation : Au moins un caractère et un chiffre
        return !string.IsNullOrWhiteSpace(password) && password.Any(char.IsLetter) && password.Any(char.IsDigit);
    }
}