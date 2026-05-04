using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class MetalMark : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<MetalMarkAnalysis> Analyses { get; set; } = new List<MetalMarkAnalysis>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
