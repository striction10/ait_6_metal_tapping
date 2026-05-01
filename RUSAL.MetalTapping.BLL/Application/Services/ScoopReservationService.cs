using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ScoopReservationService(ScoopUsageService scoopUsageService)
{
    private readonly ScoopUsageService _scoopUsageService = scoopUsageService;

    /// <summary>
    /// Резервация ковша на время выполнения задания
    /// </summary>
    /// <param name="scoopId"> Идентификатор ковша </param>
    /// <param name="busyFrom"> С какого момента ковш занят </param>
    /// <param name="busyUntil"> До какого момента ковш занят </param>
    public async Task ReservateScoop(Guid scoopId, DateTime busyFrom, DateTime busyUntil)
    {
        var currentUsage = await _scoopUsageService.GetByScoopIdAsync(scoopId);

        if (currentUsage == null)
        {
            await _scoopUsageService.CreateAsync(new ScoopUsageDto
            {
                Id = Guid.NewGuid(),
                ScoopId = scoopId,
                BusyFrom = busyFrom,
                BusyUntil = busyUntil
            });

            return;
        }

        currentUsage.BusyFrom = busyFrom;
        currentUsage.BusyUntil = busyUntil;

        await _scoopUsageService.UpdateAsync(currentUsage);
    }
}