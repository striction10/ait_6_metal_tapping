using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class DeviationValues : IDomain
    {
        public Guid Id { get; set; }
        public Guid DeviationId { get; set; }
        public int Value { get; set; }
        public int CastingRatio { get; set; }
    }
}