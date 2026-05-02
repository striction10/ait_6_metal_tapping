using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Enums;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор обработки отклонения и расчёта задания для электролизёра.
/// </summary>
/// <param name="deviationService">Сервис для работы с отклонениями.</param>
/// <param name="deviationValuesService">Сервис для работы со значениями отклонений.</param>
/// <param name="externalDataService">Сервис для работы с внешними данными.</param>
/// <param name="potParametersService">Сервис для работы с параметрами ковша.</param>
/// <param name="calculatedTaskService">Сервис для работы с рассчитанными заданиями.</param>
/// <param name="deviationCalc">Сервис расчёта отклонений.</param>
/// <param name="taskCalc">Сервис расчёта заданий.</param>
/// <param name="potParamService">Сервис получения параметров ковша.</param>
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
    private readonly DeviationService deviationService = deviationService;
    private readonly DeviationValuesService deviationValuesService = deviationValuesService;
    private readonly ExternalDataService externalDataService = externalDataService;
    private readonly PotParametersService potParametersService = potParametersService;
    private readonly CalculatedTaskService calculatedTaskService = calculatedTaskService;

    private readonly DeviationCalculationService deviationCalc = deviationCalc;
    private readonly CalculatedTaskService taskCalc = taskCalc;
    private readonly PotParametersService potParamService = potParamService;

    /// <summary>
    /// Расчёт расчётного задания и ЗПР для электролизёра.
    /// </summary>
    /// <param name="model"> Данные для расчёта. </param>
    /// <returns> ViewModel расчётного задания и ЗПР для клиента. </returns>
    public async Task<ProcessDeviationAndTaskResponse> ExecuteAsync(ProcessDeviationAndTaskRequest model)
    {
        var deviation = await deviationService.GetWithPotIdAsync(model.potId);

        var deviationValues = await deviationValuesService.GetWithDeviationIdAsync(deviation.Id);

        var deviationAmount = deviationCalc.CalculateDeviation(
            deviation.TargetMetalLevel,
            model.actualMetalLevel);

        var castingRatio = deviationCalc.GetCastingRatio(deviationAmount, deviationValues);

        if (castingRatio == null)
        {
            deviation.IsValid = false;
            deviation.ActualMetalLevel = model.actualMetalLevel;

            await deviationService.UpdateAsync(deviation);

            await calculatedTaskService.CreateAsync(new CalculatedTaskDto
            {
                Id = Guid.NewGuid(),
                PotId = model.potId,
                CalculatedTaskForPot = null,
                RoundCalculatedTaskForPot = null,
                CreatedAt = DateTime.UtcNow,
            });

            return new ProcessDeviationAndTaskResponse(deviationAmount);
        }

        deviation.IsValid = true;
        deviation.ActualMetalLevel = model.actualMetalLevel;

        var externalData = await externalDataService.GetByPotId(model.potId);

        var potParameters = await potParametersService.GetByGroupId(externalData.PotParametersGroupId);

        var amperage = potParamService.GetParameter(potParameters, PotParametersType.Amperage);
        var averageAmperage = potParamService.GetParameter(potParameters, PotParametersType.AverageAmperage);

        var calculatedTask = taskCalc.CalculatedTask(amperage, averageAmperage);
        var roundCalculatedTask = taskCalc.CalculateRoundedTask(calculatedTask, castingRatio.Value);

        await deviationService.UpdateAsync(deviation);

        await calculatedTaskService.CreateAsync(new CalculatedTaskDto
        {
            Id = Guid.NewGuid(),
            PotId = model.potId,
            CalculatedTaskForPot = calculatedTask,
            RoundCalculatedTaskForPot = roundCalculatedTask,
            CreatedAt = DateTime.UtcNow,
        });

        return new ProcessDeviationAndTaskResponse(
            deviationAmount,
            calculatedTask,
            roundCalculatedTask);
    }
}
