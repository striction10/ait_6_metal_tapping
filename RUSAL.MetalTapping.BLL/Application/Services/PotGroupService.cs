using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с группами электролизёров.
/// </summary>
/// <param name="potGroupRepository">Репозиторий групп электролизёров.</param>
/// <param name="mapper">Маппер объектов.</param>
public class PotGroupService(
    IPotGroupRepository potGroupRepository,
    IMapper mapper)
{
    private readonly IPotGroupRepository potGroupRepository = potGroupRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Получение списка групп электролизёров по идентификатору корпуса.
    /// </summary>
    /// <param name="buildingId"> Идентификатор копруса. </param>
    /// <returns> DTO групп эллектролизёров. </returns>
    public async Task<IEnumerable<PotGroupDto?>> GetByBuildingIdAsync(Guid buildingId)
    {
        var entities = EnsureFound(
            await potGroupRepository.GetByBuildingIdsAsync(buildingId),
            $"Pot groups for builiding {buildingId} was not found");

        return mapper.Map<IEnumerable<PotGroupDto?>>(entities);
    }
}
