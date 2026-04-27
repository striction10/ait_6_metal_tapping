using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.BLL.Application.Services;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class ProcessDeviationAndTaskUseCase(
    IDeviationRepository deviationRepository,
    IDeviationValuesRepository deviationValuesRepository,
    IExternalDataRepository externalDataRepository,
    IPotParametersRepository potParametersRepository,
    ICalculatedTaskRepository calculatedTaskRepository,
    DeviationCalculationService deviationCalc,
    CalculatedTaskService taskCalc,
    PotParametersService potParamService)
{
    private readonly IDeviationRepository _deviationRepository = deviationRepository;
    private readonly IDeviationValuesRepository _deviationValuesRepository = deviationValuesRepository;
    private readonly IExternalDataRepository _externalDataRepository = externalDataRepository;
    private readonly IPotParametersRepository _potParametersRepository = potParametersRepository;
    private readonly ICalculatedTaskRepository _calculatedTaskRepository = calculatedTaskRepository;

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
        var deviation = EnsureFound(
            await _deviationRepository.GetDeviationWithPotIdAsync(model.potId),
            $"Deviation with pot id {model.potId} was not found");

        var deviationValues = EnsureFound(
            await _deviationValuesRepository.GetDeviationValuesWithDeviationId(deviation.Id),
            $"DeviationValues with deviation id {deviation.Id} was not found");

        var deviationAmount = _deviationCalc.CalculateDeviation(
            deviation.TargetMetalLevel,
            model.actualMetalLevel);

        var castingRatio = _deviationCalc.GetCastingRatio(deviationAmount, deviationValues);

        if (castingRatio == null)
        {
            deviation.IsValid = false;
            deviation.ActualMetalLevel = model.actualMetalLevel;

            await _deviationRepository.UpdateAsync(deviation);

            await _calculatedTaskRepository.CreateAsync(new CalculatedTaskDto
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

        var externalData = EnsureFound(
            await _externalDataRepository.GetExternalDataWithPotId(model.potId),
            $"ExternalData with pot id {model.potId} was not found");

        var potParameters = EnsureFound(
            await _potParametersRepository.GetPotParametersWithGroupId(externalData.PotParametersGroupId),
            $"PotParameters with potGroupId {model.potId} was not found");

        var amperage = _potParamService.GetParameter(potParameters, PotParametersType.Amperage);
        var averageAmperage = _potParamService.GetParameter(potParameters, PotParametersType.AverageAmperage);

        var calculatedTask = _taskCalc.CalculatedTask(amperage, averageAmperage);
        var roundCalculatedTask = _taskCalc.CalculateRoundedTask(calculatedTask, castingRatio.Value);

        await _deviationRepository.UpdateAsync(deviation);

        await _calculatedTaskRepository.CreateAsync(new CalculatedTaskDto
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