namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class ShiftTaskDto
{
    public Guid Id { get; set; }
    public Guid TapTaskId { get; set; }
    public Guid ShiftId { get; set; }
    public DateTime LeadTime { get; set; }
}
