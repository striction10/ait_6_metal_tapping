using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class DeviationValues : IEntity
{
    public Guid Id { get; set; }
    public Guid DeviationId { get; set; }
    public int Value { get; set; }
    public int CastingRatio { get; set; }
    public Deviation Deviation { get; set; } = null!;
}