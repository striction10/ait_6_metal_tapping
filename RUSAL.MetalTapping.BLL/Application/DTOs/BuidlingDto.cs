namespace RUSAL.MetalTapping.BLL.Application.DTOs;

public class BuildingDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<PotGroupDto> Groups { get; set; }
}