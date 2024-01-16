public class CongeDTO
{
    public int IdEmploye { get; set; }
    public double Duree { get; set; }
    public DateTime Date { get; set; }

    public CongeDTO() { }

    public CongeDTO(Conge conge)
    {
        IdEmploye = conge.IdEmploye;
        Duree = conge.Duree;
        Date = conge.Date;
    }
}