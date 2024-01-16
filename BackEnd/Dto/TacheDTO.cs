public class TacheDTO
{
    public double Duree { get; set; }
    public string Description { get; set; }
    public DateTime Echeance { get; set; }

    public TacheDTO() { }

    public TacheDTO(Tache tache)
    {
        Duree = tache.Duree;
        Description = tache.Description;
        Echeance = tache.Echeance;
    }
}