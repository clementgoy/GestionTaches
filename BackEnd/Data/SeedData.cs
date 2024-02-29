public static class SeedData
{
    public static void Init()
    {
        BackendContext context = new BackendContext();
        if (context.Taches.Any())
        {
            return;
        }
        Tache tache1 = new Tache
        {
            Id = 1,
            Duree = 5.8,
            Description = "créer une API pour une application de gestion de tâches",
            Echeance = DateTime.Parse("2024-12-14"),
        };
        Tache tache2 = new Tache
        {
            Id = 2,
            Duree = 6.7,
            Description = "remplir la base de donnée avec des examples de taches",
            Echeance = DateTime.Parse("2024-04-05"),
        };
        Tache tache3 = new Tache
        {
            Id = 3,
            Duree = 7.1,
            Description = "orgniser le prochain wes",
            Echeance = DateTime.Parse("2025-01-13"),
        };

        context.AddRange(tache1, tache2, tache3);

        if (context.Employes.Any())
        {
            return;
        }
        Employe employe1 = new Employe
        {
            Id = 1,
            Nom = "Goy",
            Prenom = "Clement",
            Email = "cgoy001@ensc.fr",
            Statut = StatutEmploye.Manager,
            Pole = PoleEntreprise.Logistique,
        };
        employe1.SetMotDePasse("cg1");
        Employe employe2 = new Employe
        {
            Id = 2,
            Nom = "Dupont",
            Prenom = "François",
            Email = "dfrancois@ensc.fr",
            Statut = StatutEmploye.MembreEquipe,
            Pole = PoleEntreprise.Logistique,
        };
        employe2.SetMotDePasse("cfw156lm");
        Employe employe3 = new Employe
        {
            Id = 3,
            Nom = "Valleran",
            Prenom = "Sebastien",
            Email = "svalleran002@ensc.fr",
            MotDePasseHash = BCrypt.Net.BCrypt.HashPassword("jmwd151413"),
            Statut = StatutEmploye.MembreEquipe,
            Pole = PoleEntreprise.Logistique,
        };
        employe3.SetMotDePasse("jmwd151413");

        context.AddRange(employe1, employe2, employe3);

        context.SaveChanges();
    }


}
