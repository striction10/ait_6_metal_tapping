using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class ExternalData : IEntity
{
    public Guid Id { get; set; }
    public Guid PotId { get; set; }
    public Guid PotParametersGroupId { get; set; }
    public DateTime DateOfReceipt { get; set; }
    public Pot Pot { get; set; } = null!;
    public PotParametersGroup Parameters { get; set; } = null!;
}
