public class CongeDTO
{
    public int Id { get; set; }
    public int IdEmploye { get; set; }
    public double Duree { get; set; }
    public DateTime Date { get; set; }

    public CongeDTO() { }

    public CongeDTO(Conge conge)
    {
        Id = conge.Id;
        IdEmploye = conge.IdEmploye;
        Duree = conge.Duree;
        Date = conge.Date;
    }
}