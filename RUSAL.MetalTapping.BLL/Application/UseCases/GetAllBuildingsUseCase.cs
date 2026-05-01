using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;

public class GetAllBuildingsUseCase(BuildingService buildingService)
{
    private readonly BuildingService _buildingService = buildingService;

    /// <summary>
    /// Получение списка всех корпусов
    /// </summary>
    /// <returns> Список всех корпусов </returns>
    public async Task<IEnumerable<BuildingDto>> ExecuteAsync()
    {
        var buildings = await _buildingService.GetAllAsync();

        return buildings.Select(b => new BuildingDto
        {
            Id = b.Id,
            Name = b.Name
        });
    }
}