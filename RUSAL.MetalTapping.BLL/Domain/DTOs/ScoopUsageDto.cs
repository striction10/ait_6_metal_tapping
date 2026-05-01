namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class ScoopUsageDto
{
    public Guid Id { get; set; }
    public Guid ScoopId { get; set; }
    public DateTime BusyFrom { get; set; }
    public DateTime BusyUntil { get; set; }
}
