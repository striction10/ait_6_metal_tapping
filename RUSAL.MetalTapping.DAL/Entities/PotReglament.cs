using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class PotReglament : IEntity
{
    public Guid Id { get; set; }
    public Guid ReglamentId { get; set; }
    public Guid PotId { get; set; }
    public Reglament Reglament { get; set; } = null!;
    public Pot Pot { get; set; } = null!;
    public ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();
}
