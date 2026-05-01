using AutoMapper;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class BuildingService(
    BuildingMetalInfoService buildingService,
    IGenericRepository<Building> buildingRepository,
    IMapper mapper)
{
    private readonly BuildingMetalInfoService _buildingService = buildingService;
    private readonly IGenericRepository<Building> _buildingRepository = buildingRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Создание ViewModel
    /// </summary>
    /// <param name="building"></param>
    /// <param name="groups"></param>
    /// <param name="metalMarkId"></param>
    /// <returns> Полная информация по корпусу в виде ViewModel </returns>
    public BuildingMetalInfoViewModel CreateViewModel(
        BuildingDto building,
        List<PotGroupViewModel> groups,
        Guid metalMarkId)
    {
        var dto = new BuildingViewModel
        {
            Id = building.Id,
            Name = building.Name,
            Groups = groups
        };

        return _buildingService.AnalyzeBuilding(dto, metalMarkId);
    }

    /// <summary>
    /// Получение корпуса по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор корпуса </param>
    /// <returns> DTO корпуса </returns>
    public async Task<BuildingDto> GetByIdAsync(Guid id)
    {
        var entity = EnsureFound(await _buildingRepository.GetByIdAsync(id),
            $"Building with id {id} was not found");

        return _mapper.Map<BuildingDto>(entity);
    }

    public async Task<IEnumerable<BuildingDto>> GetAllAsync()
    {
        var entities = await _buildingRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<BuildingDto>>(entities);
    }
}