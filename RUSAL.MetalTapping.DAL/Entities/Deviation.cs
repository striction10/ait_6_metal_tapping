using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class Deviation : IEntity
{
    public Guid Id { get; set; }
    public double? ActualMetalLevel { get; set; }
    public double TargetMetalLevel { get; set; }
    public Guid PotReglamentId { get; set; }
    public bool? IsValid { get; set; }
    public PotReglament PotReglament { get; set; } = null!;
    public ICollection<DeviationValues>  DeviationValues { get; set; } = new List<DeviationValues>();
}
