using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
using ChemicalElemDto = RUSAL.MetalTapping.BLL.Domain.DTOs.ChemicalElemDto;
using PotDto = RUSAL.MetalTapping.BLL.Domain.DTOs.PotDto;
using ScoopDto = RUSAL.MetalTapping.BLL.Domain.DTOs.ScoopDto;

namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ViewTaskService(
    ITasksRepository tasksRepository,
    ITapTaskPotRepository tapTaskPotRepository,
    IMetalMarkAnalysisRepository metalMarkAnalysisRepository,
    IMetalMarkRepository metalMarkRepository,
    IGenericRepository<ScoopDto> scoopRepository,
    IGenericRepository<ChemicalElemDto> chemicalElemRepository,
    IGenericRepository<PotDto> potRepository,
    IGenericRepository<TapTaskDto> tapTaskRepository)
{
    private readonly ITasksRepository _tasksRepository = tasksRepository;
    private readonly ITapTaskPotRepository _tapTaskPotRepository = tapTaskPotRepository;
    private readonly IMetalMarkAnalysisRepository _metalMarkAnalysisRepository = metalMarkAnalysisRepository;
    private readonly IMetalMarkRepository _metalMarkRepository = metalMarkRepository;
    private readonly IGenericRepository<ScoopDto> _scoopRepository = scoopRepository;
    private readonly IGenericRepository<ChemicalElemDto> _chemicalElemRepository = chemicalElemRepository;
    private readonly IGenericRepository<PotDto> _potRepository = potRepository;
    private readonly IGenericRepository<TapTaskDto> _tapTaskRepository = tapTaskRepository;

    /// <summary>
    /// Создание ViewModel заданий на выливку для клиента
    /// </summary>
    /// <param name="request"> Задания </param>
    /// <returns> ViewModel для конкретного корпуса по сменам </returns>
    public async Task<DailyTaskResponseViewModel> ViewTask(TaskRequest request)
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

        var summary = new DailySummaryViewModel
        {
            TotalWeight = nightBlock.TotalWeight + dayBlock.TotalWeight,
            MetalGrade = nightBlock.Items.FirstOrDefault()?.MetalGrade
                         ?? dayBlock.Items.FirstOrDefault()?.MetalGrade
                         ?? "N/A"
        };

        return new DailyTaskResponseViewModel
        {
            Date = request.Date,
            NightShift = nightBlock,
            DayShift = dayBlock,
            SummaryViewModel = summary
        };
    }

    /// <summary>
    /// Создание задания на выливку конкретного электролизёра для смены
    /// </summary>
    /// <param name="tasks"> Задания на выливку</param>
    /// <returns> Задание на выливку конкретного электролизёра для смены</returns>
    private async Task<ShiftTaskBlockViewModel> BuildShiftBlock(IEnumerable<ShiftTaskDto> tasks)
    {
        var block = new ShiftTaskBlockViewModel();
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

                block.Items.Add(new ShiftTaskItemViewModel
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

    /// <summary>
    /// Создание блока таблицы с химическим составом металла внутри электролизёра
    /// </summary>
    /// <param name="metalMarkAnalysisId"> Идентификатор анализа марки металла внутри электролизёра </param>
    /// <returns> Значения состава металла </returns>
    private async Task<List<ViewModels.ChemicalElemViewModel>> BuildChemicalElements(Guid metalMarkAnalysisId)
    {
        var values = await _metalMarkAnalysisRepository
            .GetValuesByAnalysisIdAsync(metalMarkAnalysisId);

        var result = new List<ViewModels.ChemicalElemViewModel>();

        foreach (var v in values)
        {
            var elem = await _chemicalElemRepository.GetByIdAsync(v.ChemicalElemId);

            result.Add(new ViewModels.ChemicalElemViewModel
            {
                Name = elem.Name,
                Value = v.Value
            });
        }

        return result;
    }
}