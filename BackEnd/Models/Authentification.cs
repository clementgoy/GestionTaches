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
        Employe employe = _userRepository.GetUserByEmail(email);

        if (employe != null)
        {
            // Utiliser BCrypt pour vérifier si le mot de passe correspond
            return BCrypt.Net.BCrypt.Verify(password, employe.MotDePasseHash);
        }

        return false; // Utilisateur non trouvé
    }
}

public interface EmployeAuthentification
{
    Employe GetUserByEmail(string email);
}

