namespace RUSAL.MetalTapping.DAL.Models;

public class ScoopUsageModel
{
    public Guid Id { get; set; }
    public Guid ScoopId { get; set; }
    public DateTime? BusyFrom { get; set; }
    public DateTime? BusyUntil { get; set; }
    public ScoopModel Scoop { get; set; }
}
