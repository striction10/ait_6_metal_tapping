using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class ShiftTask : IEntity
{
    public Guid Id { get; set; }
    public Guid TapTaskId { get; set; }
    public Guid ShiftId { get; set; }
    public DateTime LeadTime { get; set; }
    public Shift Shift { get; set; } = null!;
    public TapTask TapTask { get; set; } = null!;
}
