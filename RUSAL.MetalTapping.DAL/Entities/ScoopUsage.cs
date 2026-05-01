using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class ScoopUsage : IEntity
{
    public Guid Id { get; set; }
    public Guid ScoopId { get; set; }
    public DateTime? BusyFrom { get; set; }
    public DateTime? BusyUntil { get; set; }
    public Scoop Scoop { get; set; } = null!;
}
