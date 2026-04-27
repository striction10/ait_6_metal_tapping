namespace RUSAL.MetalTapping.DAL.Models;

public class Order
{
    public Guid Id { get; set; }
    public double WeightOfMetal { get; set; }
    public Guid MetalMarkId { get; set; }
    public DateTime DateOfOrder { get; set; }
<<<<<<< Updated upstream:RUSAL.MetalTapping.DAL/Models/OrderModel.cs
    public MetalMarkModel MetalMark { get; set; } = null!;
    public ICollection<TapTaskModel> TapTasks { get; set; } = new List<TapTaskModel>();
=======
    public MetalMark MetalMark { get; set; } = null!;
    public ICollection<TapTask> TapTasks { get; set; } = new List<TapTask>();
>>>>>>> Stashed changes:RUSAL.MetalTapping.DAL/Entities/Order.cs
}