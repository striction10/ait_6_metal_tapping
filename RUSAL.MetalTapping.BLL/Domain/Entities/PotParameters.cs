using RUSAL.MetalTapping.BLL.Domain.Enums;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class PotParameters
    {
        public Guid Id { get; set; }
        public PotParametersType Type { get; set; }
        public double Value { get; set; }
        public Guid PotParametersGroupId { get; set; }
    }
}