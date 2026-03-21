using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class ScoopUsage : IDomain
    {
        public Guid Id { get; set; }
        public Guid ScoopId { get; set; }
        public DateTime BusyFrom { get; set; }
        public DateTime BusyUntil { get; set; }
    }
}
