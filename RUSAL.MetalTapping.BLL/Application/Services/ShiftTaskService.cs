using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ShiftTaskService(
    IShiftRepository shiftRepository,
    ITasksRepository tasksRepository,
    ScoopReservationService scoopReservationService)
{
    private readonly IShiftRepository _shiftRepository = shiftRepository;
    private readonly ITasksRepository _tasksRepository = tasksRepository;
    private readonly ScoopReservationService _scoopReservationService = scoopReservationService;

    /// <summary>
    /// Создание задания на выливку и резервация ковша
    /// </summary>
    /// <param name="tapTask"> Задание на выливку </param>
    /// <param name="shift"> Текущая смена </param>
    /// <param name="leadTime"> Время выполнения </param>
    /// <param name="countOfPots"> Количество электролизёров в задании</param>
    /// <returns> Задание на выливку </returns>
    /// <exception cref="BusinessException"> Нет следующей смены для текущей смены - перенос задания невозможен </exception>
    public async Task<ShiftTask> CreateAsync(TapTask tapTask, Shift shift, DateTime? leadTime, int countOfPots)
    {
        if (tapTask.BuildingId != shift.BuildingId)
            throw new BusinessException($"Cannot create task for shift {shift.Id} with tapTask {tapTask.Id}");

        var now = DateTime.Now;
        var nextHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local)
            .AddHours(1);

        var shiftBegin = shift.BeginDate;
        var shiftEnd = shift.EndDate;

        DateTime finalLeadTime = leadTime ?? nextHour;

        var duration = TimeSpan.FromHours(countOfPots);

        if (finalLeadTime + duration > shiftEnd)
        {
            var nextShift = EnsureFound(
                await _shiftRepository.GetNextShiftForBuilding(shift.BuildingId, shift.EndDate),
                $"Next shift from shift {shift.Id} not found");

            finalLeadTime = DateTime.SpecifyKind(nextShift.BeginDate, DateTimeKind.Local);
        }

        await _scoopReservationService.ReservateScoop(
            tapTask.ScoopId,
            finalLeadTime,
            finalLeadTime.Add(duration)
        );

        var task = new ShiftTask
        {
            Id = Guid.NewGuid(),
            TapTaskId = tapTask.Id,
            ShiftId = shift.Id,
            LeadTime = finalLeadTime
        };

        await _tasksRepository.CreateAsync(task);

        return task;
    }
}