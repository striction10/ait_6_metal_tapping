using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class ShiftTask : IDomain
{
    public Guid Id { get; set; }
    public Guid TapTaskId { get; set; }
    public Guid ShiftId { get; set; }
    public DateTime LeadTime { get; set; }
}