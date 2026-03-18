using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class Scoop : IDomain
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid BuildingId { get; set; }
        public Guid StateId { get; set; }
    }
}