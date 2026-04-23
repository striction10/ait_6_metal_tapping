using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class GroupService
{
    /// <summary>
    /// Создание DTO
    /// </summary>
    /// <param name="group"> Группа электролизёров </param>
    /// <param name="scoop"> Ковш внутри группы </param>
    /// <param name="scoopState"> Состояние ковша внутри группы </param>
    /// <param name="scoopUsage"> Состояние занятости ковша внутри группы </param>
    /// <param name="pots"> Электролизёры внутри группы </param>
    /// <returns> DTO </returns>
    public PotGroupDto Create(
        PotGroup group,
        Scoop scoop,
        ScoopState scoopState,
        ScoopUsage scoopUsage,
        List<PotDto> pots)
    {
        var isBusy = scoopUsage != null && scoopUsage.BusyUntil > DateTime.UtcNow;

        var scoopDto = new ScoopDto
        {
            Id = scoop.Id,
            State = scoopState.Name,
            IsBusy = isBusy
        };

        return new PotGroupDto
        {
            Id = group.Id,
            Scoop = scoopDto,
            Pots = pots
        };
    }
}