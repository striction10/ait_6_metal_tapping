namespace RUSAL.MetalTapping.DAL.Models
{
    public class MetalMark
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<MetalMarkAnalysis> Analyses { get; set; } = new List<MetalMarkAnalysis>();
        public ICollection<Order>  Orders { get; set; } = new List<Order>();
    }
}