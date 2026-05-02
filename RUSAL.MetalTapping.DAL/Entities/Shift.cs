using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class Shift : IEntity
{
    public Guid Id { get; set; }
    public Guid WorkGroupId { get; set; }
    public Guid BuildingId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public Building Building { get; set; } = null!;
    public WorkGroup WorkGroup { get; set; } = null!;
    public ICollection<ShiftTask> Tasks { get; set; } = null!;
}
