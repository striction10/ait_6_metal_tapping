using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис назначения заданий на выливку по сменам.
/// </summary>
/// <param name="shiftService">Сервис для работы со сменами.</param>
/// <param name="tasksService">Сервис для работы с заданиями.</param>
/// <param name="shiftTaskService">Сервис для работы со связями смен и заданий.</param>
/// <param name="tapTaskPotService">Сервис для работы со связями заданий и электролизёров.</param>
/// <param name="scoopUsageService">Сервис для работы с занятостью ковшей.</param>
public class ShiftAssignmentService(
    ShiftService shiftService,
    TasksService tasksService,
    ShiftTaskService shiftTaskService,
    TapTaskPotService tapTaskPotService,
    ScoopUsageService scoopUsageService)
{
    /// <summary>
    /// Создание задания на смену.
    /// </summary>
    /// <param name="tapTasks"> Задания на выливку. </param>
    /// <exception cref="BusinessException"> Смена на сегодняшний день отсуствует в бд. </exception>
    public async Task AssignTaskAsync(IEnumerable<TapTaskDto> tapTasks)
    {
        var currentShifts = EnsureFound(
            await shiftService.GetCurrentShifts(),
            "Current shifts was not found");

        foreach (var tapTask in tapTasks)
        {
            var shift = currentShifts.FirstOrDefault(cs => cs.BuildingId == tapTask.BuildingId)
                ?? throw new BusinessException($"No current shift found for {tapTask.BuildingId}");

            var pots = await tapTaskPotService.GetByTapTaskIdAsync(tapTask.Id);
            var countOfPots = pots.Count();

            var suitableShift = await FindSuitableShift(tapTask, shift, countOfPots);

            await shiftTaskService.CreateAsync(tapTask, suitableShift, null, countOfPots);
        }
    }

    /// <summary>
    /// Найти свободную смену для выполнения задания.
    /// </summary>
    /// <param name="task"> Задание на выливку. </param>
    /// <param name="currentShift"> Текущая смена. </param>
    /// <param name="countOfPots"> Количество электролизеров в задании. </param>
    /// <returns> Свободная смена для выполнения задания. </returns>
    /// <exception cref="BusinessException"> Нет свободных смен для выполнения задания в бд. </exception>
    private async Task<ShiftDto> FindSuitableShift(TapTaskDto task, ShiftDto currentShift, int countOfPots)
    {
        var shift = currentShift;

        while (shift != null)
        {
            if (await CanFitTaskIntoShift(task, countOfPots, shift))
            {
                return shift;
            }

            shift = await shiftService.GetNextShiftForBuilding(task.BuildingId, shift.EndDate);
        }

        throw new BusinessException("No suitable shifts for uploading the task");
    }

    /// <summary>
    /// Возможно ли задействовать текущую смену для выполнения задания.
    /// </summary>
    /// <param name="tapTask"> Задание на выливку. </param>
    /// <param name="countOfPots"> Количество электролизёров в задании. </param>
    /// <param name="shift"> Текущая смена. </param>
    /// <returns> Возможно ли задействовать текущую смену. </returns>
    private async Task<bool> CanFitTaskIntoShift(TapTaskDto tapTask, int countOfPots, ShiftDto shift)
    {
        var busyFrom = shift.BeginDate;

        var duration = TimeSpan.FromHours(countOfPots);
        var busyUntil = busyFrom + duration;

        if (busyUntil > shift.EndDate)
        {
            return false;
        }

        var usage = await scoopUsageService.GetByScoopIdAsync(tapTask.ScoopId);
        if (usage != null && usage.BusyUntil > busyFrom)
        {
            return false;
        }

        var tasks = await tasksService.GetByShiftIdAsync(shift.Id);

        foreach (var t in tasks)
        {
            var pots = await tapTaskPotService.GetByTapTaskIdAsync(t.TapTaskId);
            var tDuration = TimeSpan.FromHours(pots.Count());

            var tBusyFrom = t.LeadTime;
            var tBusyUntil = tBusyFrom + tDuration;

            if (tBusyFrom < busyUntil && busyFrom < tBusyUntil)
            {
                return false;
            }
        }

        return true;
    }
}
