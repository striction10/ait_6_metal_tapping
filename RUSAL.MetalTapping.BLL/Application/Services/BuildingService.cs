using AutoMapper;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с корпусами.
/// </summary>
/// <param name="buildingService"> Сервис анализа информации о корпусе. </param>
/// <param name="buildingRepository"> Репозиторий корпусов. </param>
/// <param name="mapper"> Маппер объектов. </param>
public class BuildingService(
    BuildingMetalInfoService buildingService,
    IGenericRepository<Building> buildingRepository,
    IMapper mapper)
{
    private readonly BuildingMetalInfoService buildingService = buildingService;
    private readonly IGenericRepository<Building> buildingRepository = buildingRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Создание ViewModel.
    /// </summary>
    /// <param name="building"> DTO корпуса. </param>
    /// <param name="groups"> Список групп электролизёров. </param>
    /// <param name="metalMarkId"> Идентификатор марки металла. </param>
    /// <returns> Полная информация по корпусу в виде ViewModel. </returns>
    public BuildingMetalInfoViewModel CreateViewModel(
        BuildingDto building,
        List<PotGroupViewModel> groups,
        Guid metalMarkId)
    {
        var dto = new BuildingViewModel
        {
            Id = building.Id,
            Name = building.Name,
            Groups = groups,
        };

        return buildingService.AnalyzeBuilding(dto, metalMarkId);
    }

    /// <summary>
    /// Получение корпуса по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор корпуса. </param>
    /// <returns> DTO корпуса. </returns>
    public async Task<BuildingDto> GetByIdAsync(Guid id)
    {
        var entity = EnsureFound(
            await buildingRepository.GetByIdAsync(id),
            $"Building with id {id} was not found");

        return mapper.Map<BuildingDto>(entity);
    }

    public async Task<IEnumerable<BuildingDto>> GetAllAsync()
    {
        var entities = await buildingRepository.GetAllAsync();

        return mapper.Map<IEnumerable<BuildingDto>>(entities);
    }
}
