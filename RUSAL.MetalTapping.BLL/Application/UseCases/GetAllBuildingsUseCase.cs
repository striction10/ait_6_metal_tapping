using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;

/// <summary>
/// Оркестратор получения списка всех корпусов.
/// </summary>
/// <param name="buildingService">Сервис для работы с корпусами.</param>
public class GetAllBuildingsUseCase(BuildingService buildingService)
{
    /// <summary>
    /// Получение списка всех корпусов.
    /// </summary>
    /// <returns> Список всех корпусов. </returns>
    public async Task<IEnumerable<BuildingDto>> ExecuteAsync()
    {
        var buildings = await buildingService.GetAllAsync();

        return buildings.Select(b => new BuildingDto
        {
            Id = b.Id,
            Name = b.Name,
        });
    }
}
