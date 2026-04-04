using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class PotGroupsHistory : IDomain
    {
        public Guid Id { get; set; }
        public Guid PotGroupId { get; set; }
        public Guid PotId { get; set; }
        public DateTime Date { get; set; }
    }
}