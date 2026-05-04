using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис резервирования ковшей на время выполнения заданий.
/// </summary>
/// <param name="scoopUsageService">Сервис для работы с занятостью ковшей.</param>
public class ScoopReservationService(ScoopUsageService scoopUsageService)
{
    /// <summary>
    /// Резервация ковша на время выполнения задания.
    /// </summary>
    /// <param name="scoopId"> Идентификатор ковша. </param>
    /// <param name="busyFrom"> С какого момента ковш занят. </param>
    /// <param name="busyUntil"> До какого момента ковш занят. </param>
    public async Task ReservateScoop(Guid scoopId, DateTime busyFrom, DateTime busyUntil)
    {
        var currentUsage = await scoopUsageService.GetByScoopIdAsync(scoopId);

        if (currentUsage == null)
        {
            await scoopUsageService.CreateAsync(new ScoopUsageDto
            {
                Id = Guid.NewGuid(),
                ScoopId = scoopId,
                BusyFrom = busyFrom,
                BusyUntil = busyUntil,
            });

            return;
        }

        currentUsage.BusyFrom = busyFrom;
        currentUsage.BusyUntil = busyUntil;

        await scoopUsageService.UpdateAsync(currentUsage);
    }
}
