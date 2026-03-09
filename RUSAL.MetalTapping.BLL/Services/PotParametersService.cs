using RUSAL.MetalTapping.BLL.Contracts;
using RUSAL.MetalTapping.BLL.Exceptions;
using RUSAL.MetalTapping.BLL.Enums;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.BLL.Services
{
    public class PotParametersService
    {
        private readonly IDeviationRepository _deviationRepository;
        private readonly IDeviationValuesRepository _deviationValuesRepository;
        private readonly IPotParametersRepository _potParametersRepository;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IGenericRepository<CalculatedTask> _calculatedTaskRepository;

        public PotParametersService(
            IDeviationRepository deviationRepository,
            IDeviationValuesRepository deviationValuesRepository,
            IPotParametersRepository potParametersRepository,
            IExternalDataRepository externalDataRepository,
            IGenericRepository<CalculatedTask> calculatedTaskRepository) 
        {
            _deviationRepository = deviationRepository;
            _deviationValuesRepository = deviationValuesRepository;
            _potParametersRepository = potParametersRepository;
            _externalDataRepository = externalDataRepository;
            _calculatedTaskRepository = calculatedTaskRepository;
        }

        public async Task<ProcessDeviationAndTaskResponse> ProcessDeviationAndTaskAsync(ProcessDeviationAndTaskRequest model)
        {
            var existingDeviation = await _deviationRepository.GetDeviationWithPotIdAsync(model.potId);

            if (existingDeviation == null)
            {
                throw new NotFoundException($"Deviation with pot id {model.potId} was not found");
            }

            var existingDeviationValues = await _deviationValuesRepository.GetDeviationValuesWithDeviationId(existingDeviation.Id);

            if (existingDeviationValues == null)
            {
                throw new NotFoundException($"DeviationValues with deviation id {existingDeviation.Id} was not found");
            }

            var deviationAmount = model.actualMetalLevel - existingDeviation.TargetMetalLevel;

            var matchingDeviationValue = existingDeviationValues
                .FirstOrDefault(v => v.Value == deviationAmount);

            if (matchingDeviationValue == null)
            {
                existingDeviation.IsValid = false;

                existingDeviation.ActualMetalLevel = deviationAmount;

                await _deviationRepository.UpdateAsync(existingDeviation);

                var response1 = new ProcessDeviationAndTaskResponse(
                        deviation: deviationAmount
                    );

                return response1;
            }

            existingDeviation.IsValid = true;
            existingDeviation.ActualMetalLevel = deviationAmount;

            var castingRatio = matchingDeviationValue.CastingRatio;
            var externalData = await _externalDataRepository.GetExternalDataWithPotId(model.potId);

            if (externalData == null)
            {
                throw new NotFoundException($"ExternalData with pot id {model.potId} was not found");
            }

            var potParameters = await _potParametersRepository.GetPotParametersWithGroupId(externalData.PotParametersGroupId);

            if (potParameters == null)
            {
                throw new NotFoundException($"PotParameters with potGroupId {model.potId} was not found");
            }

            var amperage = potParameters.FirstOrDefault(p => p.Name == "Amperage").Value;
            var averageAmperage = potParameters.FirstOrDefault(p => p.Name == "AverageAmperage").Value;

            var calculatedTask = ((amperage * averageAmperage * CalculateConstants.K) / 100) * CalculateConstants.hoursCount;
            calculatedTask = Math.Round(calculatedTask, 2);

            var roundCalculatedTask = (calculatedTask * castingRatio) / 100;

            var response = new ProcessDeviationAndTaskResponse(
                    deviation: deviationAmount,
                    calculatedTask: calculatedTask,
                    roundCalculatedTask: roundCalculatedTask
                );

            await _deviationRepository.UpdateAsync(existingDeviation);

            var calkTask = new CalculatedTask
            {
                Id = Guid.NewGuid(),
                PotId = model.potId,
                CalculatedTaskForPot = (decimal)calculatedTask,
                RoundCalculatedTaskForPot = (decimal)roundCalculatedTask,
                CreatedAt = DateTime.UtcNow
            };

            await _calculatedTaskRepository.CreateAsync( calkTask);

            return response;
        }

        public async Task ViewDeviationAndTaskAsync()
        {

        }
    }
}