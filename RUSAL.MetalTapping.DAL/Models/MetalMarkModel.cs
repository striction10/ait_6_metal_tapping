namespace RUSAL.MetalTapping.DAL.Models;

public class MetalMark
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
<<<<<<< Updated upstream:RUSAL.MetalTapping.DAL/Models/MetalMarkModel.cs
    public ICollection<MetalMarkAnalysisModel> Analyses { get; set; } = new List<MetalMarkAnalysisModel>();
    public ICollection<OrderModel>  Orders { get; set; } = new List<OrderModel>();
=======
    public ICollection<MetalMarkAnalysis> Analyses { get; set; } = new List<MetalMarkAnalysis>();
    public ICollection<Order>  Orders { get; set; } = new List<Order>();
>>>>>>> Stashed changes:RUSAL.MetalTapping.DAL/Entities/MetalMark.cs
}