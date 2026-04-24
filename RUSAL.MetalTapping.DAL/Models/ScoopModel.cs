namespace RUSAL.MetalTapping.DAL.Models;

public class ScoopModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StateId { get; set; }
    public ScoopStateModel ScoopState { get; set; } = null!;
    public ICollection<TapTaskModel> TapTasks { get; set; } = new List<TapTaskModel>();
    public ICollection<ScoopUsageModel> ScoopUsageModels { get; set; } = new List<ScoopUsageModel>();
}