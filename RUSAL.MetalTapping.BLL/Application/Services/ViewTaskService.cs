using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис формирования ViewModel заданий на выливку для отображения на клиенте.
/// </summary>
/// <param name="tasksService">Сервис для работы с заданиями на смену.</param>
/// <param name="tapTaskPotService">Сервис для работы со связями заданий и электролизёров.</param>
/// <param name="metalMarkAnalysisService">Сервис для работы с анализами марок металла.</param>
/// <param name="metalMarkService">Сервис для работы с марками металла.</param>
/// <param name="scoopRepository">Сервис для работы с ковшами.</param>
/// <param name="chemicalElemService">Сервис для работы с химическими элементами.</param>
/// <param name="potService">Сервис для работы с электролизёрами.</param>
/// <param name="tapTaskService">Сервис для работы с заданиями на выливку.</param>
/// <param name="metalMarkAnalysisValueService">Сервис для работы со значениями анализов марок металла.</param>
public class ViewTaskService(
    TasksService tasksService,
    TapTaskPotService tapTaskPotService,
    MetalMarkAnalysisService metalMarkAnalysisService,
    MetalMarkService metalMarkService,
    ScoopService scoopRepository,
    ChemicalElemService chemicalElemService,
    PotService potService,
    TapTaskService tapTaskService,
    MetalMarkAnalysisValueService metalMarkAnalysisValueService)
{
    private readonly TasksService tasksService = tasksService;
    private readonly TapTaskPotService tapTaskPotService = tapTaskPotService;
    private readonly MetalMarkAnalysisService metalMarkAnalysisService = metalMarkAnalysisService;
    private readonly MetalMarkService metalMarkService = metalMarkService;
    private readonly ScoopService scoopService = scoopRepository;
    private readonly ChemicalElemService chemicalElemService = chemicalElemService;
    private readonly PotService potService = potService;
    private readonly TapTaskService tapTaskService = tapTaskService;
    private readonly MetalMarkAnalysisValueService metalMarkAnalysisValueService = metalMarkAnalysisValueService;

    /// <summary>
    /// Создание ViewModel заданий на выливку для клиента.
    /// </summary>
    /// <param name="request"> Задания. </param>
    /// <returns> ViewModel для конкретного корпуса по сменам. </returns>
    public async Task<DailyTaskResponseViewModel> ViewTask(TaskRequest request)
    {
        var nightStart = request.date.AddDays(-1).Date.AddHours(20);
        var nightEnd = request.date.Date.AddHours(8);

        var dayStart = request.date.Date.AddHours(8);
        var dayEnd = request.date.Date.AddHours(20);

        var tasks = await tasksService.GetByBuildingAndDateRangeAsync(
            request.buildingId,
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
                         ?? "N/A",
        };

        return new DailyTaskResponseViewModel
        {
            Date = request.date,
            NightShift = nightBlock,
            DayShift = dayBlock,
            Summary = summary,
        };
    }

    /// <summary>
    /// Создание задания на выливку конкретного электролизёра для смены.
    /// </summary>
    /// <param name="tasks"> Задания на выливку.</param>
    /// <returns> Задание на выливку конкретного электролизёра для смены.</returns>
    private async Task<ShiftTaskBlockViewModel> BuildShiftBlock(IEnumerable<ShiftTaskDto> tasks)
    {
        var block = new ShiftTaskBlockViewModel();
        double total = 0;

        foreach (var task in tasks)
        {
            var tapTask = EnsureFound(
                await tapTaskService.GetByIdAsync(task.TapTaskId),
                $"TapTask {task.TapTaskId} not found");

            var scoop = EnsureFound(
                await scoopService.GetByIdAsync(tapTask.ScoopId),
                $"Scoop {tapTask.ScoopId} not found");

            var pots = await tapTaskPotService.GetByTapTaskIdAsync(task.TapTaskId);

            var startLeadTime = task.LeadTime;
            var currentTime = startLeadTime;

            foreach (var pot in pots)
            {
                var analysis = EnsureFound(
                    await metalMarkAnalysisService.GetByPotIdAsync(pot.PotId),
                    $"MetalMarkAnalysis for pot {pot.PotId} not found");

                var potEntity = EnsureFound(
                    await potService.GetByIdAsync(pot.PotId),
                    $"Pot with id {pot.PotId} not found");

                var mark = EnsureFound(
                    await metalMarkService.GetByIdAsync(analysis.MetalMarkId),
                    $"MetalMark {analysis.MetalMarkId} not found");

                var elements = await BuildChemicalElements(analysis.Id);

                block.Items.Add(new ShiftTaskItemViewModel
                {
                    Time = currentTime,
                    Weight = pot.PotMetalWeigth,
                    PotName = potEntity.Name,
                    ScoopName = scoop.Name,
                    MetalGrade = mark.Name,
                    elements = elements,
                });

                total += pot.PotMetalWeigth;

                currentTime += TimeSpan.FromHours(1);
            }

            block.TotalWeight = total;
        }

        return block;
    }

    /// <summary>
    /// Создание блока таблицы с химическим составом металла внутри электролизёра.
    /// </summary>
    /// <param name="metalMarkAnalysisId"> Идентификатор анализа марки металла внутри электролизёра. </param>
    /// <returns> Значения состава металла. </returns>
    private async Task<List<ChemicalElemViewModel>> BuildChemicalElements(Guid metalMarkAnalysisId)
    {
        var values = await metalMarkAnalysisValueService
            .GetByAnalysisIdAsync(metalMarkAnalysisId);

        var result = new List<ChemicalElemViewModel>();

        foreach (var v in values)
        {
            var elem = await chemicalElemService.GetByIdAsync(v.ChemicalElemId);

            result.Add(new ChemicalElemViewModel
            {
                Name = elem.Name,
                Value = v.Value,
            });
        }

        return result;
    }
}
