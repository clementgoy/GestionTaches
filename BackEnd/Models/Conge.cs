public class Conge 
{
    public int Id { get; set; }
    public int IdEmploye { get; set; }
    public double Duree { get; set; }
    public DateTime Date { get; set; }
    public Conge() { }
    public Conge(CongeDTO congeDTO, BackendContext context)
    {
        Id = congeDTO.Id;
        IdEmploye = congeDTO.IdEmploye;
        Duree = congeDTO.Duree;
        Date = congeDTO.Date;
    }
}