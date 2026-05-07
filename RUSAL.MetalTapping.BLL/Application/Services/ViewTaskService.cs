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
/// <param name="scoopService">Сервис для работы с ковшами.</param>
/// <param name="chemicalElemService">Сервис для работы с химическими элементами.</param>
/// <param name="potService">Сервис для работы с электролизёрами.</param>
/// <param name="tapTaskService">Сервис для работы с заданиями на выливку.</param>
/// <param name="metalMarkAnalysisValueService">Сервис для работы со значениями анализов марок металла.</param>
public class ViewTaskService(
    TasksService tasksService,
    TapTaskPotService tapTaskPotService,
    MetalMarkAnalysisService metalMarkAnalysisService,
    MetalMarkService metalMarkService,
    ScoopService scoopService,
    ChemicalElemService chemicalElemService,
    PotService potService,
    TapTaskService tapTaskService,
    MetalMarkAnalysisValueService metalMarkAnalysisValueService)
{
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

        var allGrades = nightBlock.GradeSummaries
            .Concat(dayBlock.GradeSummaries)
            .GroupBy(g => g.MetalGrade)
            .Select(g => new GradeSummaryViewModel
            {
                MetalGrade = g.Key,
                TotalWeight = g.Sum(x => x.TotalWeight),
            })
            .OrderByDescending(x => x.TotalWeight)
            .ToList();

        var summary = new DailySummaryViewModel
        {
            TotalWeight = nightBlock.TotalWeight + dayBlock.TotalWeight,
            MetalGrade = allGrades.FirstOrDefault()?.MetalGrade ?? "N/A",
            GradeSummaries = allGrades,
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
    /// Создание задания на выливку для смены.
    /// </summary>
    /// <param name="tasks"> Задания на выливку.</param>
    /// <returns> Задание на выливку конкретного электролизёра для смены.</returns>
    private async Task<ShiftTaskBlockViewModel> BuildShiftBlock(IEnumerable<ShiftTaskDto> tasks)
    {
        var block = new ShiftTaskBlockViewModel();

        var gradeTotals = new Dictionary<string, double>();

        foreach (var task in tasks)
        {
            var tapTask = EnsureFound(
                await tapTaskService.GetByIdAsync(task.TapTaskId),
                $"TapTask {task.TapTaskId} not found");

            var scoop = EnsureFound(
                await scoopService.GetByIdAsync(tapTask.ScoopId),
                $"Scoop {tapTask.ScoopId} not found");

            var pots = await tapTaskPotService.GetByTapTaskIdAsync(task.TapTaskId);
            var currentTime = task.LeadTime;

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
                var weight = pot.PotMetalWeigth;

                block.Items.Add(new ShiftTaskItemViewModel
                {
                    Time = currentTime,
                    Weight = weight,
                    PotName = potEntity.Name,
                    ScoopName = scoop.Name,
                    MetalGrade = mark.Name,
                    elements = elements,
                });

                gradeTotals.TryGetValue(mark.Name, out var current);
                gradeTotals[mark.Name] = current + weight;

                currentTime += TimeSpan.FromHours(1);
            }
        }

        block.GradeSummaries = gradeTotals
            .Select(kvp => new GradeSummaryViewModel
            {
                MetalGrade = kvp.Key,
                TotalWeight = kvp.Value
            })
            .OrderByDescending(x => x.TotalWeight)
            .ToList();

        block.TotalWeight = gradeTotals.Values.Sum();

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
