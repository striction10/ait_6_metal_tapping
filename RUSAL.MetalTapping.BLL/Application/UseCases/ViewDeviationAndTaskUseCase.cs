using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.Enums;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор формирования таблицы параметров электролизёров с отклонениями и заданиями для заданного корпуса.
/// </summary>
/// <param name="buildingService">Сервис для работы с корпусами.</param>
/// <param name="reglamentService">Сервис для работы с регламентами.</param>
/// <param name="potReglamentService">Сервис для работы с регламентами электролизёров.</param>
/// <param name="calculatedTaskService">Сервис для работы с расчётными заданиями.</param>
/// <param name="metalMarkAnalysisService">Сервис для работы с анализами марок металла.</param>
/// <param name="potService">Сервис для работы с электролизёрами.</param>
/// <param name="metalMarkService">Сервис для работы с марками металла.</param>
/// <param name="potParametersService">Сервис для работы с параметрами электролизёров.</param>
/// <param name="externalDataService">Сервис для работы с внешними данными.</param>
/// <param name="potViewService">Сервис формирования ViewModel электролизёра.</param>
/// <param name="potParamService">Сервис получения параметров электролизёра.</param>
public class ViewDeviationAndTaskUseCase(
    BuildingService buildingService,
    ReglamentService reglamentService,
    PotReglamentService potReglamentService,
    CalculatedTaskService calculatedTaskService,
    MetalMarkAnalysisService metalMarkAnalysisService,
    PotService potService,
    MetalMarkService metalMarkService,
    PotParametersService potParametersService,
    ExternalDataService externalDataService,
    PotViewService potViewService,
    PotParametersService potParamService)
{
    private readonly BuildingService buildingService = buildingService;
    private readonly PotService potService = potService;
    private readonly MetalMarkService metalMarkService = metalMarkService;
    private readonly ReglamentService reglamentService = reglamentService;
    private readonly PotReglamentService potReglamentService = potReglamentService;
    private readonly CalculatedTaskService calculatedTaskService = calculatedTaskService;
    private readonly MetalMarkAnalysisService metalMarkAnalysisService = metalMarkAnalysisService;
    private readonly PotParametersService potParametersService = potParametersService;
    private readonly ExternalDataService externalDataService = externalDataService;

    private readonly PotViewService potViewService = potViewService;
    private readonly PotParametersService potParamService = potParamService;

    /// <summary>
    /// Создание ViewModel для отображения таблицы параметров электролизёров в заданном корпусе.
    /// </summary>
    /// <param name="model"> Данные для отображения таблицы. </param>
    /// <returns> ViewModel для отображения таблицы. </returns>
    public async Task<ViewDeviationAndTaskResponse> ExecuteAsync(ViewDeviationAndTaskRequest model)
    {
        var building = await buildingService.GetByIdAsync(model.buildingId);

        var reglament = await reglamentService.GetByIdAsync(model.reglamentId);

        var potReglaments = await potReglamentService.GetByReglamentAndBuildingIdAsync(model.reglamentId, model.buildingId);

        var pots = new List<ViewDeviationAndTaskPot>();

        foreach (var potReglament in potReglaments)
        {
            var deviation = potReglament.Deviations.FirstOrDefault();
            if (deviation == null)
            {
                continue;
            }

            var pot = await potService.GetByIdAsync(potReglament.PotId);

            var externalData = await externalDataService.GetByPotId(pot.Id);

            var lastTask = await calculatedTaskService.GetByPotIdAsync(potReglament.PotId);

            var analysis = await metalMarkAnalysisService.GetByPotIdAsync(potReglament.PotId);

            var potParameters = await potParametersService.GetByGroupId(externalData.PotParametersGroupId);

            var amperage = potParamService.GetParameter(potParameters, PotParametersType.Amperage);
            var averageAmperage = potParamService.GetParameter(potParameters, PotParametersType.AverageAmperage);

            string metalMarkName = "N/A";

            if (analysis != null)
            {
                var metalMark = await metalMarkService.GetByIdAsync(analysis.MetalMarkId);
                metalMarkName = metalMark?.Name ?? "N/A";
            }

            var potView = potViewService.BuildPotView(
                pot,
                deviation,
                amperage,
                averageAmperage,
                lastTask,
                metalMarkName
            );

            pots.Add(potView);
        }

        return new ViewDeviationAndTaskResponse(pots);
    }
}
