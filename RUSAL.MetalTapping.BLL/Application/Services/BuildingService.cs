using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class BuildingService(BuildingMetalInfoService buildingService)
{
    private readonly BuildingMetalInfoService _buildingService = buildingService;

    /// <summary>
    /// Создание DTO
    /// </summary>
    /// <param name="building"></param>
    /// <param name="groups"></param>
    /// <param name="metalMarkId"></param>
    /// <returns></returns>
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