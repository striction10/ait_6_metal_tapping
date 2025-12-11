namespace RUSAL.MetalTapping.DAL.Models
{
    public class Scoop
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BuildingId { get; set; }
        public int StateId { get; set; }
        public Building Building { get; set; } = null!;
        public ScoopState ScoopState { get; set; } = null!;
        public ICollection<TapTask> TapTasks { get; set; } = new List<TapTask>();
    }
}