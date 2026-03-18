namespace RUSAL.MetalTapping.DAL.Models
{
    public class MetalMarkModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<MetalMarkAnalysisModel> Analyses { get; set; } = new List<MetalMarkAnalysisModel>();
        public ICollection<OrderModel>  Orders { get; set; } = new List<OrderModel>();
    }
}