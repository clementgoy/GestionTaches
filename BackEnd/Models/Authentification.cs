public class Authentification
{
    private readonly EmployeAuthentification _userRepository;

    public Authentification(EmployeAuthentification userRepository)
    {
        _userRepository = userRepository;
    }

    public bool VerifyCredentials(string email, string password)
    {
        // Récupérer l'utilisateur depuis la base de données en fonction de l'email
        Employee employee = _userRepository.GetUserByEmail(email);

        if (employee != null)
        {
            // Utiliser BCrypt pour vérifier si le mot de passe correspond
            return BCrypt.Net.BCrypt.Verify(password, employee.HashedPassword);
        }

        return false;
    }
}

public interface EmployeAuthentification
{
    Employee GetUserByEmail(string email);
}

