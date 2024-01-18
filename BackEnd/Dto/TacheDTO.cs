public class TacheDTO
{
    public int Id { get; set; }
    public double Duree { get; set; }
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