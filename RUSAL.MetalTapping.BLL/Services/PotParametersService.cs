using RUSAL.MetalTapping.BLL.Contracts;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.BLL.Enums;
using RUSAL.MetalTapping.BLL.Exceptions;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.DAL.Repositories;

namespace RUSAL.MetalTapping.BLL.Services
{
    public class PotParametersService
    {
        private readonly IDeviationRepository _deviationRepository;
        private readonly IDeviationValuesRepository _deviationValuesRepository;
        private readonly IPotParametersRepository _potParametersRepository;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly ICalculatedTaskRepository _calculatedTaskRepository;
        private readonly IGenericRepository<Building> _buildingRepository;
        private readonly IReglamentRepository _reglamentRepository;
        private readonly IPotReglamentRepository _potReglamentRepository;
        private readonly IMetalMarkAnalysisRepository _metalMarkAnalysisRepository;

        public PotParametersService(
            IDeviationRepository deviationRepository,
            IDeviationValuesRepository deviationValuesRepository,
            IPotParametersRepository potParametersRepository,
            IExternalDataRepository externalDataRepository,
            ICalculatedTaskRepository calculatedTaskRepository,
            IGenericRepository<Building> buidlingRepository,
            IReglamentRepository reglamentRepository,
            IPotReglamentRepository potReglamentRepository,
            IMetalMarkAnalysisRepository metalMarkAnalysisRepository)
        {
            _deviationRepository = deviationRepository;
            _deviationValuesRepository = deviationValuesRepository;
            _potParametersRepository = potParametersRepository;
            _externalDataRepository = externalDataRepository;
            _calculatedTaskRepository = calculatedTaskRepository;
            _buildingRepository = buidlingRepository;
            _reglamentRepository = reglamentRepository;
            _potReglamentRepository = potReglamentRepository;
            _metalMarkAnalysisRepository = metalMarkAnalysisRepository;
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
            existingDeviation.ActualMetalLevel = model.actualMetalLevel;

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
                CalculatedTaskForPot = calculatedTask,
                RoundCalculatedTaskForPot = roundCalculatedTask,
                CreatedAt = DateTime.UtcNow
            };

            await _calculatedTaskRepository.CreateAsync(calkTask);

            return response;
        }

        public async Task<ViewDeviationAndTaskResponse> ViewDeviationAndTaskAsync(ViewDeviationAndTaskRequest model)
        {
            var existingBuilding = await _buildingRepository.FindByIdAsync(model.buildingId);

            if (existingBuilding == null)
            {
                throw new NotFoundException($"Building with id {model.buildingId} was not found");
            }

            var existingReglament = await _reglamentRepository.FindByIdAsync(model.reglamentId);

            if (existingReglament == null)
            {
                throw new NotFoundException($"Reglament with id {model.reglamentId} was not found");
            }

            var existingPotDeviations = await _potReglamentRepository.GetByReglamentAndBuildingWithDeviationsAsync(model.reglamentId, model.buildingId);

            var pots = new List<ViewDeviationAndTaskPot>();

            foreach (var potReglament in existingPotDeviations)
            {
                var deviation = potReglament.Deviations.FirstOrDefault();

                var lastCalculatedTask = await _calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(potReglament.PotId);

                var metalMark = await _metalMarkAnalysisRepository.GetMetalMarkAnalysisWithPotIdAsync(potReglament.PotId);

                var actualDeviation = deviation?.TargetMetalLevel - deviation?.ActualMetalLevel;

                var pot = new ViewDeviationAndTaskPot(
                        potName: potReglament.Pot.Name,
                        targetMetalLevel: deviation.TargetMetalLevel,
                        actualMetalLevel: deviation.ActualMetalLevel ?? null,
                        deviationValue: actualDeviation ?? null,
                        calculatedTask: lastCalculatedTask?.CalculatedTaskForPot ?? null,
                        roundCalculatedTask: lastCalculatedTask?.RoundCalculatedTaskForPot ?? null,
                        metalMarkName: metalMark?.MetalMark.Name ?? "N/A"
                    );

                pots.Add(pot);
            }

            var reponse = new ViewDeviationAndTaskResponse(
                    pots: pots
                );

            return reponse;
        }
    }
}