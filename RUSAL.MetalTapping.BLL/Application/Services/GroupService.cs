using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class GroupService
{
    /// <summary>
    /// Создание DTO
    /// </summary>
    /// <param name="groupDto"> Группа электролизёров </param>
    /// <param name="scoopDtoКовш внутри группы </param>
    /// <param name="scoopStateDto"> Состояние ковша внутри группы </param>
    /// <param name="scoopUsageDto"> Состояние занятости ковша внутри группы </param>
    /// <param name="pots"> Электролизёры внутри группы </param>
    /// <returns> DTO </returns>
    public PotGroupViewModel Create(
        PotGroupDto group,
        ScoopDto scoop,
        ScoopStateDto scoopState,
        ScoopUsageDto scoopUsage,
        List<PotViewModel> pots)
    {
        var isBusy = scoopUsageDto != null && scoopUsageDto.BusyUntil > DateTime.UtcNow;

        var scoopDto = new ScoopViewModel
        {
            Id = scoop.Id,
            State = scoopStateDto.Name,
            IsBusy = isBusy
        };

        return new PotGroupViewModel
        {
            Id = groupDto.Id,
            Scoop = scoopDto,
            Pots = pots
        };
    }
}