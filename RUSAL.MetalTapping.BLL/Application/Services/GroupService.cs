using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис формирования ViewModel групп электролизёров.
/// </summary>
public class GroupService
{
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
