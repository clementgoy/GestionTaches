public class Tache
{
    public int Id { get; set; }
    public double Duree { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Echeance { get; set; }
    public Tache() { }
    public Tache(TacheDTO tacheDTO, BackendContext context)
    {
        Id = tacheDTO.Id;
        Duree = tacheDTO.Duree;
        Description = tacheDTO.Description;
        Echeance = tacheDTO.Echeance;
    }

}