namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class PotDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StateId { get; set; }
    public Guid BuildingId { get; set; }
}