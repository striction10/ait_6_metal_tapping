using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ShiftTaskService(
    ShiftService shiftService,
    TasksService tasksService,
    ScoopReservationService scoopReservationService)
{
    private readonly ShiftService _shiftService = shiftService;
    private readonly TasksService _tasksService = tasksService;
    private readonly ScoopReservationService _scoopReservationService = scoopReservationService;

    /// <summary>
    /// Создание задания на выливку и резервация ковша
    /// </summary>
    /// <param name="tapTaskDto"> Задание на выливку </param>
    /// <param name="shiftDto"> Текущая смена </param>
    /// <param name="leadTime"> Время выполнения </param>
    /// <param name="countOfPots"> Количество электролизёров в задании</param>
    /// <returns> Задание на выливку </returns>
    /// <exception cref="BusinessException"> Нет следующей смены для текущей смены - перенос задания невозможен </exception>
    public async Task<ShiftTaskDto> CreateAsync(TapTaskDto tapTask, ShiftDto shift, DateTime? leadTime, int countOfPots)
    {
        if (tapTaskDto.BuildingId != shiftDto.BuildingId)
            throw new BusinessException($"Cannot create task for shift {shiftDto.Id} with tapTask {tapTaskDto.Id}");

        var now = DateTime.Now;
        var nextHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local)
            .AddHours(1);

        var shiftBegin = shiftDto.BeginDate;
        var shiftEnd = shiftDto.EndDate;

        DateTime finalLeadTime = leadTime ?? nextHour;

        var duration = TimeSpan.FromHours(countOfPots);

        if (finalLeadTime + duration > shiftEnd)
        {
            var nextShift = await _shiftService.GetNextShiftForBuilding(shift.BuildingId, shift.EndDate);

            finalLeadTime = DateTime.SpecifyKind(nextShift.BeginDate, DateTimeKind.Local);
        }

        await _scoopReservationService.ReservateScoop(
            tapTaskDto.ScoopId,
            finalLeadTime,
            finalLeadTime.Add(duration)
        );

        var task = new ShiftTaskDto
        {
            Id = Guid.NewGuid(),
            TapTaskId = tapTaskDto.Id,
            ShiftId = shiftDto.Id,
            LeadTime = finalLeadTime
        };

        await _tasksService.CreateAsync(task);

        return task;
    }
}