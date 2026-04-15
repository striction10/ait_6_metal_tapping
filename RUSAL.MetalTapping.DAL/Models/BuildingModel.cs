namespace RUSAL.MetalTapping.DAL.Models
{
    public class BuildingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PotModel> Pots { get; set; } = new List<PotModel>();
        public ICollection<TapTaskModel> TapTasks { get; set; } = new List<TapTaskModel>();
        public ICollection<PotGroupModel> PotGroupModels { get; set; } = new List<PotGroupModel>();
        public ICollection<ShiftModel> Shifts { get; set; } = new List<ShiftModel>();
    }
}