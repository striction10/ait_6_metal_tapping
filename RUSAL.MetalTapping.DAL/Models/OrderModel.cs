namespace RUSAL.MetalTapping.DAL.Models;

public class OrderModel
{
    public Guid Id { get; set; }
    public double WeightOfMetal { get; set; }
    public Guid MetalMarkId { get; set; }
    public DateTime DateOfOrder { get; set; }
    public MetalMarkModel MetalMark { get; set; } = null!;
    public ICollection<TapTaskModel> TapTasks { get; set; } = new List<TapTaskModel>();
}