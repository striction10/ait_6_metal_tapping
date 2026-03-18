namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class TapTask
    {
        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ScoopId { get; set; }
    }
}