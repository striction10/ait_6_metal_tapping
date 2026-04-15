using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class ShiftTaskService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly ITasksRepository _tasksRepository;
        private readonly ScoopReservationService _scoopReservationService;

        public ShiftTaskService(
            IShiftRepository shiftRepository,
            ITasksRepository tasksRepository,
            IScoopUsageRepository scoopUsageRepository,
            ScoopReservationService scoopReservationService)
        {
            _shiftRepository = shiftRepository;
            _tasksRepository = tasksRepository;
            _scoopReservationService = scoopReservationService;
        }

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
}
