using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;

public class GetAllBuildingsUseCase(IGenericRepository<Building> repository)
{
    private readonly IGenericRepository<Building> _repository = repository;

    /// <summary>
    /// Получение списка всех корпусов
    /// </summary>
    /// <returns> Список всех корпусов </returns>
    public async Task<IEnumerable<BuildingDto>> ExecuteAsync()
    {
        var buildings = await _repository.GetAllAsync();

        return buildings.Select(b => new BuildingDto
        {
            Id = b.Id,
            Name = b.Name
        });
    }
}