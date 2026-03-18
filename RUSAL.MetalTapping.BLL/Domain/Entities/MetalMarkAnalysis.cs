namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class MetalMarkAnalysis
    {
        public Guid Id { get; set; }
        public Guid PotId { get; set; }
        public Guid MetalMarkId { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public List<MetalMarkAnalysisValue> Values { get; set; } = new();
    }
}
