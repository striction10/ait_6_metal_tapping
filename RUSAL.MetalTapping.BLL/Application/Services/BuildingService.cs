using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.Entities;

public class BuildingService
{
    private readonly BuildingMetalInfoService _buildingService;

    public BuildingService(BuildingMetalInfoService buildingService)
    {
        _buildingService = buildingService;
    }

    public BuildingMetalInfo Create(
        Building building,
        List<PotGroupDto> groups,
        Guid metalMarkId)
    {
        var dto = new BuildingDto
        {
            Id = building.Id,
            Name = building.Name,
            Groups = groups
        };

        return _buildingService.AnalyzeBuilding(dto, metalMarkId);
    }
}