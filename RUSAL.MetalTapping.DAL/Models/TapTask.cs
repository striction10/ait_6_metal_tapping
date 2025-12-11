namespace RUSAL.MetalTapping.DAL.Models
{
    public class TapTask
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int OrderId { get; set; }
        public int ScoopId { get; set; }
        public Scoop Scoop { get; set; } = null!;
        public Building Building { get; set; } = null!;
        public Order Order { get; set; } = null!;
        public ICollection<TapTaskPot> TapTaskPots = new List<TapTaskPot>();
    }
}