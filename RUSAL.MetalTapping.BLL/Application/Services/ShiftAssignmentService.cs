using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Interfaces;
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
    public async Task AssignTaskAsync(IEnumerable<TapTaskDto> tapTasks)
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
    /// <param name="taskDto"> Задание на выливку </param>
    /// <param name="currentShiftDto"> Текущая смена </param>
    /// <param name="countOfPots"> Количество электролизеров в задании </param>
    /// <returns> Свободная смена для выполнения задания </returns>
    /// <exception cref="BusinessException"> Нет свободных смен для выполнения задания в бд </exception>
    private async Task<ShiftDto> FindSuitableShift(TapTaskDto taskDto, ShiftDto currentShiftDto, int countOfPots)
    {
        var shift = currentShiftDto;

        while (shift != null)
        {
            if (await CanFitTaskIntoShift(taskDto, countOfPots, shift))
                return shift;

            shift = await _shiftRepository.GetNextShiftForBuilding(taskDto.BuildingId, shift.EndDate);
        }

        throw new BusinessException("No suitable shifts for uploading the task");
    }

    /// <summary>
    /// Возможно ли задействовать текущую смену для выполнения задания
    /// </summary>
    /// <param name="tapTaskDto"> Задание на выливку </param>
    /// <param name="countOfPots"> Количество электролизёров в задании </param>
    /// <param name="shiftDto"> Текущая смена </param>
    /// <returns> Возможно ли задействовать текущую смену </returns>
    private async Task<bool> CanFitTaskIntoShift(TapTaskDto tapTaskDto, int countOfPots, ShiftDto shiftDto)
    {
        var busyFrom = shiftDto.BeginDate;

        var duration = TimeSpan.FromHours(countOfPots);
        var busyUntil = busyFrom + duration;

        if (busyUntil > shiftDto.EndDate)
            return false;

        var usage = await _scoopUsageRepository.GetByScoopIdAsync(tapTaskDto.ScoopId);
        if (usage != null && usage.BusyUntil > busyFrom)
            return false;

        var tasks = await _tasksRepository.GetByShiftIdAsync(shiftDto.Id);

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