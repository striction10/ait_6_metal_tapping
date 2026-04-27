using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using BuildingDto = RUSAL.MetalTapping.BLL.Domain.DTOs.BuildingDto;

namespace RUSAL.MetalTapping.BLL.Application.Services;

public class BuildingService(BuildingMetalInfoService buildingService)
{
    private readonly BuildingMetalInfoService _buildingService = buildingService;

    /// <summary>
    /// Создание DTO
    /// </summary>
    /// <param name="buildingDto"></param>
    /// <param name="groups"></param>
    /// <param name="metalMarkId"></param>
    /// <returns></returns>
    public BuildingMetalInfoViewModel Create(
        BuildingDto buildingDto,
        List<PotGroupViewModel> groups,
        Guid metalMarkId)
    {
        var dto = new ViewModels.BuildingDto
        {
            Id = buildingDto.Id,
            Name = buildingDto.Name,
            Groups = groups
        };

        return _buildingService.AnalyzeBuilding(dto, metalMarkId);
    }
}