using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class Pot : IDomain
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid StateId { get; set; }
        public Guid BuildingId { get; set; }
    }
}
