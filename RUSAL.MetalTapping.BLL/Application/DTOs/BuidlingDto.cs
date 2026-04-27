namespace RUSAL.MetalTapping.BLL.Application.DTOs;

public class BuildingDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<PotGroupViewModel> Groups { get; set; }
}