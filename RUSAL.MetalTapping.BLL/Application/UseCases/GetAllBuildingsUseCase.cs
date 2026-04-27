using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Interfaces;
using BuildingDto = RUSAL.MetalTapping.BLL.Domain.DTOs.BuildingDto;

namespace RUSAL.MetalTapping.BLL.Application.UseCases.Buildings;

public class GetAllBuildingsUseCase(IGenericRepository<BuildingDto> repository)
{
    private readonly IGenericRepository<BuildingDto> _repository = repository;

    /// <summary>
    /// Получение списка всех корпусов
    /// </summary>
    /// <returns> Список всех корпусов </returns>
    public async Task<IEnumerable<ViewModels.BuildingDto>> ExecuteAsync()
    {
        var buildings = await _repository.GetAllAsync();

        return buildings.Select(b => new ViewModels.BuildingDto
        {
            Id = b.Id,
            Name = b.Name
        });
    }
}