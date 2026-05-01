namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class ShiftDto
{
    public Guid Id { get; set; }
    public Guid WorkGroupId { get; set; }
    public Guid BuildingId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
}