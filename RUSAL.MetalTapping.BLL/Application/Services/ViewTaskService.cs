using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class ViewTaskService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ITapTaskPotRepository _tapTaskPotRepository;
        private readonly IMetalMarkAnalysisRepository _metalMarkAnalysisRepository;
        private readonly IMetalMarkRepository _metalMarkRepository;
        private readonly IGenericRepository<Scoop> _scoopRepository;
        private readonly IGenericRepository<ChemicalElem> _chemicalElemRepository;
        private readonly IGenericRepository<Pot> _potRepository;
        private readonly IGenericRepository<TapTask> _tapTaskRepository;

        public ViewTaskService(
            ITasksRepository tasksRepository,
            ITapTaskPotRepository tapTaskPotRepository,
            IMetalMarkAnalysisRepository metalMarkAnalysisRepository,
            IMetalMarkRepository metalMarkRepository,
            IGenericRepository<Scoop> scoopRepository,
            IGenericRepository<ChemicalElem> chemicalElemRepository,
            IGenericRepository<Pot> potRepository,
            IGenericRepository<TapTask> tapTaskRepository)
        {
            _tasksRepository = tasksRepository;
            _tapTaskPotRepository = tapTaskPotRepository;
            _metalMarkAnalysisRepository = metalMarkAnalysisRepository;
            _metalMarkRepository = metalMarkRepository;
            _scoopRepository = scoopRepository;
            _chemicalElemRepository = chemicalElemRepository;
            _potRepository = potRepository;
            _tapTaskRepository = tapTaskRepository;
        }

        public async Task<DailyTaskResponse> ViewTask(TaskRequest request)
        {
            var nightStart = request.Date.AddDays(-1).Date.AddHours(20);
            var nightEnd = request.Date.Date.AddHours(8);

            var dayStart = request.Date.Date.AddHours(8);
            var dayEnd = request.Date.Date.AddHours(20);

            var tasks = await _tasksRepository.GetByBuildingAndDateRange(
                request.BuildingId,
                nightStart,
                dayEnd
            );

            var nightTasks = tasks.Where(t => t.LeadTime >= nightStart && t.LeadTime < nightEnd);
            var dayTasks = tasks.Where(t => t.LeadTime >= dayStart && t.LeadTime < dayEnd);

            var nightBlock = await BuildShiftBlock(nightTasks);
            var dayBlock = await BuildShiftBlock(dayTasks);

            var summary = new DailySummary
            {
                TotalWeight = nightBlock.TotalWeight + dayBlock.TotalWeight,
                MetalGrade = nightBlock.Items.FirstOrDefault()?.MetalGrade
                             ?? dayBlock.Items.FirstOrDefault()?.MetalGrade
                             ?? "N/A"
            };

            return new DailyTaskResponse
            {
                Date = request.Date,
                NightShift = nightBlock,
                DayShift = dayBlock,
                Summary = summary
            };
        }

        private async Task<ShiftTaskBlock> BuildShiftBlock(IEnumerable<ShiftTask> tasks)
        {
            var block = new ShiftTaskBlock();
            double total = 0;

            foreach (var task in tasks)
            {
                var tapTask = EnsureFound(
                    await _tapTaskRepository.GetByIdAsync(task.TapTaskId),
                    $"TapTask {task.TapTaskId} not found");

                var scoop = EnsureFound(
                    await _scoopRepository.GetByIdAsync(tapTask.ScoopId),
                    $"Scoop {tapTask.ScoopId} not found");

                var pots = await _tapTaskPotRepository.GetByTapTaskId(task.TapTaskId);

                var startLeadTime = task.LeadTime;
                var currentTime = startLeadTime;

                foreach (var pot in pots)
                {
                    var analysis = EnsureFound(
                        await _metalMarkAnalysisRepository.GetMetalMarkAnalysisWithPotIdAsync(pot.PotId),
                        $"MetalMarkAnalysis for pot {pot.PotId} not found");

                    var potEntity = EnsureFound(
                        await _potRepository.GetByIdAsync(pot.PotId),
                        $"Pot with id {pot.PotId} not found");

                    var mark = EnsureFound(
                        await _metalMarkRepository.GetByIdAsync(analysis.MetalMarkId),
                        $"MetalMark {analysis.MetalMarkId} not found");

                    var elements = await BuildChemicalElements(analysis.Id);

                    block.Items.Add(new ShiftTaskItem
                    {
                        Time = currentTime,
                        Weight = pot.PotMetalWeigth,
                        PotName = potEntity.Name,
                        ScoopName = scoop.Name,
                        MetalGrade = mark.Name,
                        elements = elements
                    });

                    total += pot.PotMetalWeigth;

                    currentTime += TimeSpan.FromHours(1);
                }

                block.TotalWeight = total;
            }

            return block;
        }

        private async Task<List<ChemicalElemDto>> BuildChemicalElements(Guid metalMarkAnalysisId)
        {
            var values = await _metalMarkAnalysisRepository
                .GetValuesByAnalysisIdAsync(metalMarkAnalysisId);

            var result = new List<ChemicalElemDto>();

            foreach (var v in values)
            {
                var elem = await _chemicalElemRepository.GetByIdAsync(v.ChemicalElemId);

                result.Add(new ChemicalElemDto
                {
                    Name = elem.Name,
                    Value = v.Value
                });
            }

            return result;
        }
    }
}