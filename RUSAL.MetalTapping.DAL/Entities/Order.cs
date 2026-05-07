using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class Order : IEntity
{
    public Guid Id { get; set; }
    public double WeightOfMetal { get; set; }
    public Guid MetalMarkId { get; set; }
    public DateTime DateOfOrder { get; set; }
    public decimal RemainingWeight { get; set; }
    public int Status { get; set; }
    public MetalMark MetalMark { get; set; } = null!;
    public ICollection<TapTask> TapTasks { get; set; } = new List<TapTask>();
}
