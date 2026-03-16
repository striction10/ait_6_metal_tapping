namespace RUSAL.MetalTapping.DAL.Models
{
    public class TapTaskModel
    {
        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ScoopId { get; set; }
        public ScoopModel Scoop { get; set; } = null!;
        public BuildingModel Building { get; set; } = null!;
        public OrderModel Order { get; set; } = null!;
        public ICollection<TapTaskPotModel> TapTaskPots = new List<TapTaskPotModel>();
    }
}