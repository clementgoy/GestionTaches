using System;
using System.ComponentModel.DataAnnotations;
public class TacheDTO
{
    public int Id { get; set; }
    public double Duree { get; set; }

    [Required(ErrorMessage = "La description est obligatoire")]
    [StringLength(100, ErrorMessage = "Le nom doit contenir entre 3 et 200 caractères", MinimumLength = 3)]
    public string Description { get; set; }
    public DateTime Echeance { get; set; }

    public TacheDTO() { }

    public TacheDTO(Tache tache)
    {
        Id = tache.Id;
        Duree = tache.Duree;
        Description = tache.Description;
        Echeance = tache.Echeance;
    }
}