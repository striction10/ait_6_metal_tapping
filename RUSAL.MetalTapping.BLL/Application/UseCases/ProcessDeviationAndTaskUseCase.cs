using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.BLL.Application.Services;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
using RUSAL.MetalTapping.BLL.Application.Contracts;

namespace RUSAL.MetalTapping.BLL.Application.UseCases
{
    public class ProcessDeviationAndTaskUseCase
    {
        private readonly IDeviationRepository _deviationRepository;
        private readonly IDeviationValuesRepository _deviationValuesRepository;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IPotParametersRepository _potParametersRepository;
        private readonly ICalculatedTaskRepository _calculatedTaskRepository;

        private readonly DeviationCalculationService _deviationCalc;
        private readonly CalculatedTaskService _taskCalc;
        private readonly PotParametersService _potParamService;

        public ProcessDeviationAndTaskUseCase(
            IDeviationRepository deviationRepository,
            IDeviationValuesRepository deviationValuesRepository,
            IExternalDataRepository externalDataRepository,
            IPotParametersRepository potParametersRepository,
            ICalculatedTaskRepository calculatedTaskRepository,
            DeviationCalculationService deviationCalc,
            CalculatedTaskService taskCalc,
            PotParametersService potParamService)
        {
            _deviationRepository = deviationRepository;
            _deviationValuesRepository = deviationValuesRepository;
            _externalDataRepository = externalDataRepository;
            _potParametersRepository = potParametersRepository;
            _calculatedTaskRepository = calculatedTaskRepository;

            _deviationCalc = deviationCalc;
            _taskCalc = taskCalc;
            _potParamService = potParamService;
        }

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
                deviation.ActualMetalLevel = deviationAmount;

                await _deviationRepository.UpdateAsync(deviation);

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

            await _calculatedTaskRepository.CreateAsync(new CalculatedTask
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
}