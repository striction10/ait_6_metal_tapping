using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис создания заданий для смен с резервированием ковшей.
/// </summary>
/// <param name="shiftService">Сервис для работы со сменами.</param>
/// <param name="tasksService">Сервис для работы с заданиями.</param>
/// <param name="scoopReservationService">Сервис резервирования ковшей.</param>
public class ShiftTaskService(
    ShiftService shiftService,
    TasksService tasksService,
    ScoopReservationService scoopReservationService)
{
    private readonly ShiftService shiftService = shiftService;
    private readonly TasksService tasksService = tasksService;
    private readonly ScoopReservationService scoopReservationService = scoopReservationService;

    /// <summary>
    /// Создание задания на выливку и резервация ковша.
    /// </summary>
    /// <param name="tapTask"> Задание на выливку. </param>
    /// <param name="shift"> Текущая смена. </param>
    /// <param name="leadTime"> Время выполнения. </param>
    /// <param name="countOfPots"> Количество электролизёров в задании.</param>
    /// <returns> Задание на выливку. </returns>
    /// <exception cref="BusinessException"> Нет следующей смены для текущей смены - перенос задания невозможен. </exception>
    public async Task<ShiftTaskDto> CreateAsync(TapTaskDto tapTask, ShiftDto shift, DateTime? leadTime, int countOfPots)
    {
        if (tapTask.BuildingId != shift.BuildingId)
        {
            throw new BusinessException($"Cannot create task for shift {shift.Id} with tapTask {tapTask.Id}");
        }

        var now = DateTime.Now;
        var nextHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local)
            .AddHours(1);

        var shiftBegin = shift.BeginDate;
        var shiftEnd = shift.EndDate;

        DateTime finalLeadTime = leadTime ?? nextHour;

        var duration = TimeSpan.FromHours(countOfPots);

        if (finalLeadTime + duration > shiftEnd)
        {
            var nextShift = await shiftService.GetNextShiftForBuilding(shift.BuildingId, shift.EndDate);

            finalLeadTime = DateTime.SpecifyKind(nextShift.BeginDate, DateTimeKind.Local);
        }

        await scoopReservationService.ReservateScoop(
            tapTask.ScoopId,
            finalLeadTime,
            finalLeadTime.Add(duration)
        );

        var task = new ShiftTaskDto
        {
            Id = Guid.NewGuid(),
            TapTaskId = tapTask.Id,
            ShiftId = shift.Id,
            LeadTime = finalLeadTime,
        };

        await tasksService.CreateAsync(task);

        return task;
    }
}
