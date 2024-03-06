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
            Duree = 5.8,
            Description = "créer une API pour une application de gestion de tâches",
            Echeance = DateTime.Parse("2024-12-14"),
        };
        Tache tache2 = new Tache
        {
            Duree = 6.7,
            Description = "remplir la base de donnée avec des examples de taches",
            Echeance = DateTime.Parse("2024-04-05"),
        };
        Tache tache3 = new Tache
        {
            Duree = 7.1,
            Description = "orgniser le prochain wes",
            Echeance = DateTime.Parse("2025-01-13"),
        };
        var tache4 = new Tache
        {
            Duree = 8.5,
            Description = "Finaliser le rapport mensuel",
            Echeance = DateTime.Now.AddDays(5),
        };
        var tache5 = new Tache
        {
            Duree = 2,
            Description = "Préparer la présentation du client",
            Echeance = DateTime.Now.AddDays(10),
        };

        context.AddRange(tache1, tache2, tache3, tache4, tache5);

        if (context.Employes.Any())
        {
            return;
        }
        Employe employe1 = new Employe
        {
            Nom = "Goy",
            Prenom = "Clement",
            Email = "cgoy001@ensc.fr",
            Statut = StatutEmploye.Manager,
            Pole = PoleEntreprise.Logistique,
            ResetToken = "",
            ResetTokenExpires = DateTime.Parse("2024-03-02"),
        };
        employe1.SetMotDePasse("cg1");
        Employe employe2 = new Employe
        {
            Nom = "Dupont",
            Prenom = "François",
            Email = "dfrancois@ensc.fr",
            Statut = StatutEmploye.MembreEquipe,
            Pole = PoleEntreprise.Logistique,
            ResetToken = "",
            ResetTokenExpires = DateTime.Parse("2024-03-02"),
        };
        employe2.SetMotDePasse("cfw156lm");
        Employe employe3 = new Employe
        {
            Nom = "Valleran",
            Prenom = "Sebastien",
            Email = "svalleran002@ensc.fr",
            Statut = StatutEmploye.MembreEquipe,
            Pole = PoleEntreprise.Logistique,
            ResetToken = "",
            ResetTokenExpires = DateTime.Parse("2024-03-02"),
        };
        employe3.SetMotDePasse("jmwd151413");
        var employe4 = new Employe
        {
            Nom = "Doe",
            Prenom = "John",
            Email = "john.doe@example.com",
            Statut = StatutEmploye.MembreEquipe,
            Pole = PoleEntreprise.Logistique,
            ResetToken = "",
            ResetTokenExpires = DateTime.Parse("2024-03-02"),
        };
        employe4.SetMotDePasse("mdp1");
        var employe5 = new Employe
        {
            Nom = "Smith",
            Prenom = "Jane",
            Email = "jane.smith@example.com",
            Statut = StatutEmploye.MembreEquipe,
            Pole = PoleEntreprise.Logistique,
            ResetToken = "",
            ResetTokenExpires = DateTime.Parse("2024-03-02"),
        };
        employe5.SetMotDePasse("mdp3");

        context.AddRange(employe1, employe2, employe3, employe4, employe5);

        if (context.Assignations.Any())
        {
            return;
        }
        var assignation1 = new Assignation
        {
            Message = "A faire le plus vite possible, quitte à mettre en pause les autres taches",
        };
        assignation1.SetIds(1, 1);
        var assignation2 = new Assignation
        {
            Message = "A faire le plus vite possible, quitte à mettre en pause les autres taches",
        };
        assignation2.SetIds(1, 3);
        var assignation3 = new Assignation
        {
            Message = "A faire le plus vite possible, quitte à mettre en pause les autres taches",
        };
        assignation3.SetIds(2, 4);
        var assignation4 = new Assignation
        {
            Message = "A faire le plus vite possible, quitte à mettre en pause les autres taches",
        };
        assignation4.SetIds(3, 2);
        var assignation5 = new Assignation
        {
            Message = "A faire le plus vite possible, quitte à mettre en pause les autres taches",
        };
        assignation5.SetIds(3, 3);
        var assignation6 = new Assignation
        {
            Message = "A faire le plus vite possible, quitte à mettre en pause les autres taches",
        };
        assignation6.SetIds(5, 4);

        context.Assignations.AddRange(assignation1, assignation2, assignation3, assignation4, assignation5, assignation6);

        var conge1 = new Conge
        {
            Date = DateTime.Now.AddDays(30),
            Duree = 2,
            Motif = "Vacances",
        };
        conge1.SetId(1);
        var conge2 = new Conge
        {
            Date = DateTime.Now.AddDays(60),
            Duree = 1.8,
            Motif = "Congé personnel",
        };
        conge2.SetId(2);
        var conge3 = new Conge
        {
            Date = DateTime.Now.AddDays(60),
            Duree = 5.8,
            Motif = "Voyage",
        };
        conge3.SetId(3);
        var conge4 = new Conge
        {
            Date = DateTime.Now.AddDays(60),
            Duree = 60,
            Motif = "Congé sabattique",
        };
        conge4.SetId(1);

        context.Conges.AddRange(conge1, conge2, conge3, conge4);

        context.SaveChanges();
    }
}
