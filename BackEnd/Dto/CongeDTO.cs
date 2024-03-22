using System.ComponentModel.DataAnnotations;
public class CongeDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "L'id de l'employé (haché) est obligatoire")]
    public int IdEmploye { get; set; }

    [Required(ErrorMessage = "La durée est obligatoire")]
    public double Duree { get; set; }

    [Required(ErrorMessage = "La date de début est obligatoire")]
    public DateTime Date { get; set; }

    [StringLength(100, ErrorMessage = "Le motif doit contenir entre 3 et 100 caractères", MinimumLength = 3)]
    public string Motif { get; set; }

    public CongeDTO() { }

    public CongeDTO(Conge conge)
    {
        Id = conge.Id;
        IdEmploye = conge.IdEmploye;
        Duree = conge.Duree;
        Date = conge.Date;
        Motif = conge.Motif;
    }
}