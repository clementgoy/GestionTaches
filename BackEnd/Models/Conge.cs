public class Conge 
{
    public int Id { get; set; }
    public int IdEmploye { get; set; }
    public double Duree { get; set; }
    public DateTime Date { get; set; }
    public Conge() { }
    public Conge(CongeDTO congeDTO, BackendContext context)
    {
        IdEmploye = congeDTO.IdEmploye;
        Duree = congeDTO.Duree;
        Date = congeDTO.Date;
    }
}