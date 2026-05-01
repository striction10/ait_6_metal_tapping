using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class ProcessDeviationAndTaskUseCase(
    DeviationService deviationService,
    DeviationValuesService deviationValuesService,
    ExternalDataService externalDataService,
    PotParametersService potParametersService,
    CalculatedTaskService calculatedTaskService,
    DeviationCalculationService deviationCalc,
    CalculatedTaskService taskCalc,
    PotParametersService potParamService)
{
    private readonly DeviationService _deviationService = deviationService;
    private readonly DeviationValuesService _deviationValuesService = deviationValuesService;
    private readonly ExternalDataService _externalDataService = externalDataService;
    private readonly PotParametersService _potParametersService = potParametersService;
    private readonly CalculatedTaskService _calculatedTaskService = calculatedTaskService;

    private readonly DeviationCalculationService _deviationCalc = deviationCalc;
    private readonly CalculatedTaskService _taskCalc = taskCalc;
    private readonly PotParametersService _potParamService = potParamService;

    /// <summary>
    /// Расчёт расчётного задания и ЗПР для электролизёра
    /// </summary>
    /// <param name="model"> Данные для расчёта </param>
    /// <returns> ViewModel расчётного задания и ЗПР для клиента </returns>
    public async Task<ProcessDeviationAndTaskResponse> ExecuteAsync(ProcessDeviationAndTaskRequest model)
    {
        var deviation = await _deviationService.GetWithPotIdAsync(model.potId);

        var deviationValues = await _deviationValuesService.GetWithDeviationIdAsync(deviation.Id);

        var deviationAmount = _deviationCalc.CalculateDeviation(
            deviation.TargetMetalLevel,
            model.actualMetalLevel);

        var castingRatio = _deviationCalc.GetCastingRatio(deviationAmount, deviationValues);

        if (castingRatio == null)
        {
            deviation.IsValid = false;
            deviation.ActualMetalLevel = model.actualMetalLevel;

            await _deviationService.UpdateAsync(deviation);

            await _calculatedTaskService.CreateAsync(new CalculatedTaskDto
            {
                Id = Guid.NewGuid(),
                PotId = model.potId,
                CalculatedTaskForPot = null,
                RoundCalculatedTaskForPot = null,
                CreatedAt = DateTime.UtcNow
            });

            return new ProcessDeviationAndTaskResponse(deviationAmount);
        }

        deviation.IsValid = true;
        deviation.ActualMetalLevel = model.actualMetalLevel;

        var externalData = await _externalDataService.GetByPotId(model.potId);

        var potParameters = await _potParametersService.GetByGroupId(externalData.PotParametersGroupId);

        var amperage = _potParamService.GetParameter(potParameters, PotParametersType.Amperage);
        var averageAmperage = _potParamService.GetParameter(potParameters, PotParametersType.AverageAmperage);

        var calculatedTask = _taskCalc.CalculatedTask(amperage, averageAmperage);
        var roundCalculatedTask = _taskCalc.CalculateRoundedTask(calculatedTask, castingRatio.Value);

        await _deviationService.UpdateAsync(deviation);

        await _calculatedTaskService.CreateAsync(new CalculatedTaskDto
        {
            Id = Guid.NewGuid(),
            PotId = model.potId,
            CalculatedTaskForPot = calculatedTask,
            RoundCalculatedTaskForPot = roundCalculatedTask,
            CreatedAt = DateTime.UtcNow
        });

        return new ProcessDeviationAndTaskResponse(
            deviationAmount,
            calculatedTask,
            roundCalculatedTask);
    }
}