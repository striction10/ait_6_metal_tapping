namespace RUSAL.MetalTapping.DAL.Models
{
    public class ScoopModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid BuildingId { get; set; }
        public Guid StateId { get; set; }
        public BuildingModel Building { get; set; } = null!;
        public ScoopStateModel ScoopState { get; set; } = null!;
        public ICollection<TapTaskModel> TapTasks { get; set; } = new List<TapTaskModel>();
        public ICollection<ScoopUsageModel> ScoopUsageModels { get; set; } = new List<ScoopUsageModel>();
    }
}