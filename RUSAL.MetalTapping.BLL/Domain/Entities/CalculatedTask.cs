using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class CalculatedTask : IDomain
    {
        public Guid Id { get; set; }
        public Guid PotId { get; set; }
        public double? CalculatedTaskForPot { get; set; }
        public double? RoundCalculatedTaskForPot { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}