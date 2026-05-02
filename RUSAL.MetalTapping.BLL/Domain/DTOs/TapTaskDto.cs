namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class TapTaskDto
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ScoopId { get; set; }
}
