using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using PotGroupDto = RUSAL.MetalTapping.BLL.Domain.DTOs.PotGroupDto;
using ScoopDto = RUSAL.MetalTapping.BLL.Domain.DTOs.ScoopDto;

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
    public ViewModels.PotGroupViewModel Create(
        PotGroupDto groupDto,
        ScoopDto scoopDto,
        ScoopStateDto scoopStateDto,
        ScoopUsageDto scoopUsageDto,
        List<PotViewModel> pots)
    {
        var isBusy = scoopUsageDto != null && scoopUsageDto.BusyUntil > DateTime.UtcNow;

        var scoopDto = new ViewModels.ScoopViewModel
        {
            Id = scoop.Id,
            State = scoopStateDto.Name,
            IsBusy = isBusy
        };

        return new ViewModels.PotGroupViewModel
        {
            Id = groupDto.Id,
            Scoop = scoopDto,
            Pots = pots
        };
    }
}