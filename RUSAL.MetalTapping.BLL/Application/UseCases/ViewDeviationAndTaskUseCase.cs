using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.UseCases
{
    public class ViewDeviationAndTaskUseCase
    {
        private readonly IGenericRepository<Building> _buildingRepository;
        private readonly IGenericRepository<Pot> _potRepository;
        private readonly IGenericRepository<MetalMark> _metalMarkRepository;
        private readonly IReglamentRepository _reglamentRepository;
        private readonly IPotReglamentRepository _potReglamentRepository;
        private readonly ICalculatedTaskRepository _calculatedTaskRepository;
        private readonly IMetalMarkAnalysisRepository _metalMarkAnalysisRepository;
        private readonly IPotParametersRepository _potParametersRepository;
        private readonly IExternalDataRepository _externalDataRepository;

        private readonly PotViewService _potViewService;
        private readonly PotParametersService _potParamService;

        public ViewDeviationAndTaskUseCase(
            IGenericRepository<Building> buildingRepository,
            IReglamentRepository reglamentRepository,
            IPotReglamentRepository potReglamentRepository,
            ICalculatedTaskRepository calculatedTaskRepository,
            IMetalMarkAnalysisRepository metalMarkAnalysisRepository,
            IGenericRepository<Pot> potRepository,
            IGenericRepository<MetalMark> metalMarkRepository,
            IPotParametersRepository potParametersRepository,
            IExternalDataRepository externalDataRepository,
            PotViewService potViewService,
            PotParametersService potParamService)
        {
            _buildingRepository = buildingRepository;
            _reglamentRepository = reglamentRepository;
            _potReglamentRepository = potReglamentRepository;
            _calculatedTaskRepository = calculatedTaskRepository;
            _metalMarkAnalysisRepository = metalMarkAnalysisRepository;
            _potRepository = potRepository;
            _metalMarkRepository = metalMarkRepository;
            _potParametersRepository = potParametersRepository;
            _externalDataRepository = externalDataRepository;
            _potViewService = potViewService;
            _potParamService = potParamService;
        }

        public async Task<ViewDeviationAndTaskResponse> ExecuteAsync(ViewDeviationAndTaskRequest model)
        {
            EnsureFound(await _buildingRepository.GetByIdAsync(model.buildingId),
                $"Building with id {model.buildingId} was not found");

            EnsureFound(await _reglamentRepository.GetByIdAsync(model.reglamentId),
                $"Reglament with id {model.reglamentId} was not found");

            var potReglaments = await _potReglamentRepository
                .GetByReglamentAndBuildingWithDeviationsAsync(model.reglamentId, model.buildingId);

            var pots = new List<ViewDeviationAndTaskPot>();

            foreach (var potReglament in potReglaments)
            {
                var deviation = potReglament.Deviations.FirstOrDefault();
                if (deviation == null)
                    continue;

                var pot = EnsureFound(await _potRepository.GetByIdAsync(potReglament.PotId),
                    $"Pot with id {potReglament.PotId} was not found");

                var externalData = EnsureFound(await _externalDataRepository.GetExternalDataWithPotId(pot.Id),
                    $"ExternalData with pot id {pot.Id} was not found");

                var lastTask = await _calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(potReglament.PotId);

                var analysis = await _metalMarkAnalysisRepository.GetMetalMarkAnalysisWithPotIdAsync(potReglament.PotId);

                var potParameters = EnsureFound(await _potParametersRepository.GetPotParametersWithGroupId(externalData.PotParametersGroupId),
                    $"PotParameters with potGroupId {pot.Id} was not found");

                var amperage = _potParamService.GetParameter(potParameters, PotParametersType.Amperage);
                var averageAmperage = _potParamService.GetParameter(potParameters, PotParametersType.AverageAmperage);

                string metalMarkName = "N/A";

                if (analysis != null)
                {
                    var metalMark = await _metalMarkRepository.GetByIdAsync(analysis.MetalMarkId);
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
}