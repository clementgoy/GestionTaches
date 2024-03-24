public class Authentification
{
    private readonly EmployeAuthentification _userRepository;

    public Authentification(EmployeAuthentification userRepository)
    {
        _userRepository = userRepository;
    }

    public bool VerifyCredentials(string email, string password)
    {
        // Get user from database based on email
        Employee employee = _userRepository.GetUserByEmail(email);

        if (employee != null)
        {
            // Use BCrypt to check if the password matches
            return BCrypt.Net.BCrypt.Verify(password, employee.HashedPassword);
        }

        return false;
    }
}

public interface EmployeAuthentification
{
    Employee GetUserByEmail(string email);
}

