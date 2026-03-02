namespace RUSAL.MetalTapping.DAL.Models
{
    public class TapTask
    {
        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ScoopId { get; set; }
        public Scoop Scoop { get; set; } = null!;
        public Building Building { get; set; } = null!;
        public Order Order { get; set; } = null!;
        public ICollection<TapTaskPot> TapTaskPots = new List<TapTaskPot>();
    }
}