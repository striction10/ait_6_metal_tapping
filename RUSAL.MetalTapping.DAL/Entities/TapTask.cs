using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class TapTask : IEntity
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ScoopId { get; set; }
    public Scoop Scoop { get; set; } = null!;
    public Building Building { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public ICollection<ShiftTask> Tasks { get; set; } = new List<ShiftTask>();
    public ICollection<TapTaskPot> TapTaskPots { get; set; } = new List<TapTaskPot>();
}