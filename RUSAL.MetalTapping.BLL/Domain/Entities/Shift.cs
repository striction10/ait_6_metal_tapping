using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class Shift : IDomain
    {
        public Guid Id { get; set; }
        public Guid WorkGroupId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}