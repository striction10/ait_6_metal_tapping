using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class ShiftAssignmentService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly ITasksRepository _tasksRepository;
        private readonly ITapTaskPotRepository _tapTaskPotRepository;
        
        private readonly ShiftTaskService _shiftTaskService;

        public ShiftAssignmentService(
            IShiftRepository shiftRepository,
            ITasksRepository tasksRepository,
            ShiftTaskService shiftTaskService,
            ITapTaskPotRepository tapTaskPotRepository)
        {
            _shiftRepository = shiftRepository;
            _tasksRepository = tasksRepository;
            _shiftTaskService = shiftTaskService;
            _tapTaskPotRepository = tapTaskPotRepository;
        }

        public async Task AssignTaskAsync(IEnumerable<TapTask> tapTasks)
        {
            var currentShifts = EnsureFound(
                await _shiftRepository.GetCurrentShifts(),
                "Current shifts was not found");

            foreach (var tapTask in tapTasks)
            {
                var shift = currentShifts.FirstOrDefault(cs => cs.BuildingId == tapTask.BuildingId);

                if (shift == null)
                {
                    throw new BusinessException($"No current shift found for {tapTask.BuildingId}");
                }

                var tapTaskPots = await _tapTaskPotRepository.GetByTapTaskId(tapTask.Id);

                var suitableShift = await FindSuitableShift(tapTask, shift, tapTaskPots.Count());

                await _shiftTaskService.CreateAsync(tapTask, suitableShift, null, tapTaskPots.Count());
            }
        }

        private async Task<Shift?> FindSuitableShift(TapTask task, Shift currentShift, int countOfPots)
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

        private async Task<bool> CanFitTaskIntoShift(TapTask tapTask, int countOfPots, Shift shift)
        {
            var now = DateTime.UtcNow;
            var nextHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Utc)
                .AddHours(1);

            var tasks = EnsureFound(await _tasksRepository.GetByShiftIdAsync(shift.Id),
                $"Task with shift id {shift.Id} was not found");

            var duration = TimeSpan.FromHours(countOfPots);

            if (shift.EndDate < nextHour + duration)
                return false;

            if (tasks.Any(t => t.LeadTime == nextHour))
                return false;

            return true;
        }
    }
}