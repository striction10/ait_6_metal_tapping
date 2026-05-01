using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.Enums;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

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
    private readonly BuildingService _buildingService = buildingService;
    private readonly PotService _potService = potService;
    private readonly MetalMarkService _metalMarkService = metalMarkService;
    private readonly ReglamentService _reglamentService = reglamentService;
    private readonly PotReglamentService _potReglamentService = potReglamentService;
    private readonly CalculatedTaskService _calculatedTaskService = calculatedTaskService;
    private readonly MetalMarkAnalysisService _metalMarkAnalysisService = metalMarkAnalysisService;
    private readonly PotParametersService _potParametersService = potParametersService;
    private readonly ExternalDataService _externalDataService = externalDataService;

    private readonly PotViewService _potViewService = potViewService;
    private readonly PotParametersService _potParamService = potParamService;

    /// <summary>
    /// Создание ViewModel для отображения таблицы параметров электролизёров в заданном корпусе
    /// </summary>
    /// <param name="model"> Данные для отображения таблицы </param>
    /// <returns> ViewModel для отображения таблицы </returns>
    public async Task<ViewDeviationAndTaskResponse> ExecuteAsync(ViewDeviationAndTaskRequest model)
    {
        var building = await _buildingService.GetByIdAsync(model.buildingId);

        var reglament = await _reglamentService.GetByIdAsync(model.reglamentId);

        var potReglaments = await _potReglamentService.GetByReglamentAndBuildingIdAsync(model.reglamentId, model.buildingId);

        var pots = new List<ViewDeviationAndTaskPot>();

        foreach (var potReglament in potReglaments)
        {
            var deviation = potReglament.Deviations.FirstOrDefault();
            if (deviation == null)
                continue;

            var pot = await _potService.GetByIdAsync(potReglament.PotId);

            var externalData = await _externalDataService.GetByPotId(pot.Id);

            var lastTask = await _calculatedTaskService.GetByPotIdAsync(potReglament.PotId);

            var analysis = await _metalMarkAnalysisService.GetByPotIdAsync(potReglament.PotId);

            var potParameters = await _potParametersService.GetByGroupId(externalData.PotParametersGroupId);

            var amperage = _potParamService.GetParameter(potParameters, PotParametersType.Amperage);
            var averageAmperage = _potParamService.GetParameter(potParameters, PotParametersType.AverageAmperage);

            string metalMarkName = "N/A";

            if (analysis != null)
            {
                var metalMark = await _metalMarkService.GetByIdAsync(analysis.MetalMarkId);
                metalMarkName = metalMark?.Name ?? "N/A";
            }

            var potView = _potViewService.BuildPotView(
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