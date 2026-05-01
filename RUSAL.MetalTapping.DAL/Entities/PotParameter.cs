using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class PotParameter : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public Guid PotParametersGroupId { get; set; }
    public PotParametersGroup Group { get; set; } = null!;
}