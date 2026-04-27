namespace RUSAL.MetalTapping.DAL.Models;

public class TapTask
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ScoopId { get; set; }
<<<<<<< Updated upstream:RUSAL.MetalTapping.DAL/Models/TapTaskModel.cs
    public ScoopModel Scoop { get; set; } = null!;
    public BuildingModel Building { get; set; } = null!;
    public OrderModel Order { get; set; } = null!;
    public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    public ICollection<TapTaskPotModel> TapTaskPots = new List<TapTaskPotModel>();
=======
    public Scoop Scoop { get; set; } = null!;
    public Building Building { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<TapTaskPot> TapTaskPots = new List<TapTaskPot>();
>>>>>>> Stashed changes:RUSAL.MetalTapping.DAL/Entities/TapTask.cs
}