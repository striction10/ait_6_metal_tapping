namespace RUSAL.MetalTapping.DAL.Models;

public class PotReglament
{
    public Guid Id { get; set; }
    public Guid ReglamentId { get; set; }
    public Guid PotId { get; set; }
<<<<<<< Updated upstream:RUSAL.MetalTapping.DAL/Models/PotReglamentModel.cs
    public ReglamentModel Reglament { get; set; } = null!;
    public PotModel Pot { get; set; } = null!;
    public ICollection<DeviationModel> Deviations { get; set; } = new List<DeviationModel>();
=======
    public Reglament Reglament { get; set; } = null!;
    public Pot Pot { get; set; } = null!;
    public ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();
>>>>>>> Stashed changes:RUSAL.MetalTapping.DAL/Entities/PotReglament.cs
}