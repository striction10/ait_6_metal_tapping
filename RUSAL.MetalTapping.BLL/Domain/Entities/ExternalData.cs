using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class ExternalData : IDomain
    {
        public Guid Id { get; set; }
        public Guid PotId { get; set; }
        public Guid PotParametersGroupId { get; set; }
        public DateTime DateOfReceipt { get; set; }
    }
}