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
            var deviation = EnsureFound(
                await _deviationRepository.GetDeviationWithPotIdAsync(model.potId),
                $"Deviation with pot id {model.potId} was not found");

            var deviationValues = EnsureFound(
                await _deviationValuesRepository.GetDeviationValuesWithDeviationId(deviation.Id),
                $"DeviationValues with deviation id {deviation.Id} was not found");

            var deviationAmount = model.actualMetalLevel - deviation.TargetMetalLevel;

            var matchingDeviationValue = deviationValues.FirstOrDefault(v => v.Value == deviationAmount);

            if (matchingDeviationValue == null)
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

            var amperage = potParameters.First(p => p.Name == "Amperage").Value;
            var averageAmperage = potParameters.First(p => p.Name == "AverageAmperage").Value; //TODO: Вынести в отдельный Enum

            var calculatedTask = Math.Round(
                ((amperage * averageAmperage * CalculateConstants.K) / 100) * CalculateConstants.hoursCount,
                2);

            var roundCalculatedTask = (calculatedTask * matchingDeviationValue.CastingRatio) / 100;

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

        public async Task<ProcessCalculatedTaskResponse> ProcessCalculatedTaskAsync(ProcessCalculatedTaskRequest model)
        {
            var calculatedTasks = await _calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(model.potId);

            if (calculatedTasks == null) 
            {
                await _calculatedTaskRepository.CreateAsync(new CalculatedTask
                {
                    Id = Guid.NewGuid(),
                    PotId = model.potId,
                    CalculatedTaskForPot = model.calculatedTask,
                    RoundCalculatedTaskForPot = model.roundedCalculatedTask ?? 0,
                    CreatedAt = DateTime.UtcNow
                });

                return new ProcessCalculatedTaskResponse(
                    calculatedTask: model.calculatedTask,
                    roundCalculatedTask: model.roundedCalculatedTask ?? 0
                );
            }

            calculatedTasks.CalculatedTaskForPot = model.calculatedTask;
            calculatedTasks.RoundCalculatedTaskForPot = model.roundedCalculatedTask ?? 0;
            calculatedTasks.CreatedAt = DateTime.UtcNow;

            await _calculatedTaskRepository.UpdateAsync(calculatedTasks);

            return new ProcessCalculatedTaskResponse(
                calculatedTask: model.calculatedTask,
                roundCalculatedTask: model.roundedCalculatedTask ?? 0
            );
        }

        public async Task<ViewDeviationAndTaskResponse> ViewDeviationAndTaskAsync(ViewDeviationAndTaskRequest model)
        {
            EnsureFound(await _buildingRepository.FindByIdAsync(model.buildingId),
                $"Building with id {model.buildingId} was not found");

            EnsureFound(await _reglamentRepository.FindByIdAsync(model.reglamentId),
                $"Reglament with id {model.reglamentId} was not found");

            var potReglaments =
                await _potReglamentRepository.GetByReglamentAndBuildingWithDeviationsAsync(
                    model.reglamentId, model.buildingId);

            var pots = new List<ViewDeviationAndTaskPot>();

            foreach (var potReglament in potReglaments)
            {
                var deviation = potReglament.Deviations.FirstOrDefault();
                if (deviation == null)
                    continue;

                var lastTask = await _calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(potReglament.PotId);
                var metalMark = await _metalMarkAnalysisRepository.GetMetalMarkAnalysisWithPotIdAsync(potReglament.PotId);

                var deviationValue = deviation.TargetMetalLevel - deviation.ActualMetalLevel;

                pots.Add(new ViewDeviationAndTaskPot(
                    potId: potReglament.PotId,
                    potName: potReglament.Pot.Name,
                    targetMetalLevel: deviation.TargetMetalLevel,
                    actualMetalLevel: deviation.ActualMetalLevel,
                    deviationValue: deviationValue,
                    calculatedTask: lastTask?.CalculatedTaskForPot,
                    roundCalculatedTask: lastTask?.RoundCalculatedTaskForPot,
                    metalMarkName: metalMark?.MetalMark?.Name ?? "N/A"
                ));
            }

            return new ViewDeviationAndTaskResponse(pots);
        }

        private static T EnsureFound<T>(T entity, string message) // TODO: Вынести в отдельный класс
        {
            if (entity == null)
                throw new NotFoundException(message);

            return entity;
        }
    }
}