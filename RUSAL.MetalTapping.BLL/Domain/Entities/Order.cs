using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class Order : IDomain
    {
        public Guid Id { get; set; }
        public double WeightOfMetal { get; set; }
        public Guid MetalmarkId { get; set; }
        DateTime DateOfOrder { get; set; }
    }
}