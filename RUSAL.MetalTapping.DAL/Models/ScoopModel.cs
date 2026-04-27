namespace RUSAL.MetalTapping.DAL.Models;

public class Scoop
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StateId { get; set; }
    public ScoopState ScoopState { get; set; } = null!;
    public ICollection<TapTask> TapTasks { get; set; } = new List<TapTask>();
    public ICollection<ScoopUsage> ScoopUsageModels { get; set; } = new List<ScoopUsage>();
}