using AutoMapper;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
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

    /// <summary>
    /// Получение группы электролизёров по идентификатору ковша.
    /// </summary>
    /// <param name="scoopId">Идентификатор ковша.</param>
    /// <returns>DTO группы.</returns>
    public async Task<PotGroupDto?> GetByScoopIdAsync(Guid scoopId)
    {
        var entity = EnsureFound(
            await potGroupRepository.GetByScoopIdAsync(scoopId),
            $"Pot group for scoop {scoopId} was not found");

        return mapper.Map<PotGroupDto>(entity);
    }

    /// <summary>
    /// Создание DTO.
    /// </summary>
    /// <param name="group"> Группа электролизёров. </param>
    /// <param name="scoop"> Ковш внутри группы. </param>
    /// <param name="scoopState"> Состояние ковша внутри группы. </param>
    /// <param name="scoopUsage"> Состояние занятости ковша внутри группы. </param>
    /// <param name="pots"> Электролизёры внутри группы. </param>
    /// <returns> DTO. </returns>
    public PotGroupViewModel Create(
        PotGroupDto group,
        ScoopDto scoop,
        ScoopStateDto scoopState,
        ScoopUsageDto scoopUsage,
        List<PotViewModel> pots)
    {
        var isBusy = scoopUsage != null && scoopUsage.BusyUntil > DateTime.UtcNow;

        var scoopDto = new ScoopViewModel
        {
            Id = scoop.Id,
            State = scoopState.Name,
            IsBusy = isBusy,
        };

        return new PotGroupViewModel
        {
            Id = group.Id,
            Scoop = scoopDto,
            Pots = pots,
        };
    }
}
