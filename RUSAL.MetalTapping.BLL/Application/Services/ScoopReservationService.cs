using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ScoopReservationService(IScoopUsageRepository scoopUsageRepository)
{
    private readonly IScoopUsageRepository _scoopUsageRepository = scoopUsageRepository;

    /// <summary>
    /// Резервация ковша на время выполнения задания
    /// </summary>
    /// <param name="scoopId"> Идентификатор ковша </param>
    /// <param name="busyFrom"> С какого момента ковш занят </param>
    /// <param name="busyUntil"> До какого момента ковш занят </param>
    public async Task ReservateScoop(Guid scoopId, DateTime busyFrom, DateTime busyUntil)
    {
        var currentUsage = await _scoopUsageRepository.GetByScoopIdAsync(scoopId);

        if (currentUsage == null)
        {
            var scoopUsage = new ScoopUsage
            {
                Id = Guid.NewGuid(),
                ScoopId = scoopId,
                BusyFrom = busyFrom,
                BusyUntil = busyUntil
            };

            await _scoopUsageRepository.CreateAsync(scoopUsage);

            return;
        }

        currentUsage.BusyFrom = busyFrom;
        currentUsage.BusyUntil = busyUntil;

        await _scoopUsageRepository.UpdateAsync(currentUsage);

        return;
    }
}