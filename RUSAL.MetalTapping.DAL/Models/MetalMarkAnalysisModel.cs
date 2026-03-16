namespace RUSAL.MetalTapping.DAL.Models
{
    public class MetalMarkAnalysisModel
    {
        public Guid Id { get; set; }
        public Guid PotId { get; set; }
        public Guid MetalMarkId { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public PotModel Pot { get; set; } = null!;
        public MetalMarkModel MetalMark { get; set; } = null!;
        public ICollection<MetalMarkAnalysisValueModel> Values { get; set; } = new List<MetalMarkAnalysisValueModel>();
        public ICollection<TapTaskPotModel> TapTaskPots { get; set; } = new List<TapTaskPotModel>();
    }
}