using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ShiftAssignmentService(
    IShiftRepository shiftRepository,
    ITasksRepository tasksRepository,
    ShiftTaskService shiftTaskService,
    ITapTaskPotRepository tapTaskPotRepository,
    IScoopUsageRepository scoopUsageRepository)
{
    private readonly IShiftRepository _shiftRepository = shiftRepository;
    private readonly ITasksRepository _tasksRepository = tasksRepository;
    private readonly ITapTaskPotRepository _tapTaskPotRepository = tapTaskPotRepository;
    private readonly IScoopUsageRepository _scoopUsageRepository = scoopUsageRepository;
    
    private readonly ShiftTaskService _shiftTaskService = shiftTaskService;

    /// <summary>
    /// Создание задания на смену
    /// </summary>
    /// <param name="tapTasks"> Задания на выливку </param>
    /// <exception cref="BusinessException"> Смена на сегодняшний день отсуствует в бд </exception>
    public async Task AssignTaskAsync(IEnumerable<TapTask> tapTasks)
    {
        var currentShifts = EnsureFound(
            await _shiftRepository.GetCurrentShifts(),
            "Current shifts was not found");

        foreach (var tapTask in tapTasks)
        {
            var shift = currentShifts.FirstOrDefault(cs => cs.BuildingId == tapTask.BuildingId)
                ?? throw new BusinessException($"No current shift found for {tapTask.BuildingId}");

            var pots = await _tapTaskPotRepository.GetByTapTaskId(tapTask.Id);
            var countOfPots = pots.Count();

            var suitableShift = await FindSuitableShift(tapTask, shift, countOfPots);

            await _shiftTaskService.CreateAsync(tapTask, suitableShift, null, countOfPots);
        }
    }

    /// <summary>
    /// Найти свободную смену для выполнения задания
    /// </summary>
    /// <param name="task"> Задание на выливку </param>
    /// <param name="currentShift"> Текущая смена </param>
    /// <param name="countOfPots"> Количество электролизеров в задании </param>
    /// <returns> Свободная смена для выполнения задания </returns>
    /// <exception cref="BusinessException"> Нет свободных смен для выполнения задания в бд </exception>
    private async Task<Shift> FindSuitableShift(TapTask task, Shift currentShift, int countOfPots)
    {
        var shift = currentShift;

        while (shift != null)
        {
            if (await CanFitTaskIntoShift(task, countOfPots, shift))
                return shift;

            shift = await _shiftRepository.GetNextShiftForBuilding(task.BuildingId, shift.EndDate);
        }

        throw new BusinessException("No suitable shifts for uploading the task");
    }

    /// <summary>
    /// Возможно ли задействовать текущую смену для выполнения задания
    /// </summary>
    /// <param name="tapTask"> Задание на выливку </param>
    /// <param name="countOfPots"> Количество электролизёров в задании </param>
    /// <param name="shift"> Текущая смена </param>
    /// <returns> Возможно ли задействовать текущую смену </returns>
    private async Task<bool> CanFitTaskIntoShift(TapTask tapTask, int countOfPots, Shift shift)
    {
        var busyFrom = shift.BeginDate;

        var duration = TimeSpan.FromHours(countOfPots);
        var busyUntil = busyFrom + duration;

        if (busyUntil > shift.EndDate)
            return false;

        var usage = await _scoopUsageRepository.GetByScoopIdAsync(tapTask.ScoopId);
        if (usage != null && usage.BusyUntil > busyFrom)
            return false;

        var tasks = await _tasksRepository.GetByShiftIdAsync(shift.Id);

        foreach (var t in tasks)
        {
            var pots = await _tapTaskPotRepository.GetByTapTaskId(t.TapTaskId);
            var tDuration = TimeSpan.FromHours(pots.Count());

            var tBusyFrom = t.LeadTime;
            var tBusyUntil = tBusyFrom + tDuration;

            if (tBusyFrom < busyUntil && busyFrom < tBusyUntil)
                return false;
        }

        return true;
    }
}