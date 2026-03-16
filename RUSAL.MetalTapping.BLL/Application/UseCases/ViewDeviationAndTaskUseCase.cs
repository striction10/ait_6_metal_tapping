using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.BLL.Application.Services;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
using RUSAL.MetalTapping.BLL.Application.Contracts;

namespace RUSAL.MetalTapping.BLL.Application.UseCases
{
    public class ViewDeviationAndTaskUseCase
    {
        private readonly IGenericRepository<Building> _buildingRepository;
        private readonly IReglamentRepository _reglamentRepository;
        private readonly IPotReglamentRepository _potReglamentRepository;
        private readonly ICalculatedTaskRepository _calculatedTaskRepository;
        private readonly IMetalMarkAnalysisRepository _metalMarkAnalysisRepository;
        private readonly IGenericRepository<Pot> _potRepository;
        private readonly IGenericRepository<MetalMark> _metalMarkRepository;

        private readonly PotViewService _potViewService;

        public ViewDeviationAndTaskUseCase(
            IGenericRepository<Building> buildingRepository,
            IReglamentRepository reglamentRepository,
            IPotReglamentRepository potReglamentRepository,
            ICalculatedTaskRepository calculatedTaskRepository,
            IMetalMarkAnalysisRepository metalMarkAnalysisRepository,
            IGenericRepository<Pot> potRepository,
            IGenericRepository<MetalMark> metalMarkRepository,
            PotViewService potViewService)
        {
            _buildingRepository = buildingRepository;
            _reglamentRepository = reglamentRepository;
            _potReglamentRepository = potReglamentRepository;
            _calculatedTaskRepository = calculatedTaskRepository;
            _metalMarkAnalysisRepository = metalMarkAnalysisRepository;
            _potRepository = potRepository;
            _metalMarkRepository = metalMarkRepository;
            _potViewService = potViewService;
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

                var pot = EnsureFound(
                    await _potRepository.GetByIdAsync(potReglament.PotId),
                    $"Pot with id {potReglament.PotId} was not found");

                var lastTask = await _calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(potReglament.PotId);

                var analysis = await _metalMarkAnalysisRepository.GetMetalMarkAnalysisWithPotIdAsync(potReglament.PotId);

                string metalMarkName = "N/A";

                if (analysis != null)
                {
                    var metalMark = await _metalMarkRepository.GetByIdAsync(analysis.MetalMarkId);
                    metalMarkName = metalMark?.Name ?? "N/A";
                }

                var potView = _potViewService.BuildPotView(
                    pot,
                    deviation,
                    lastTask,
                    metalMarkName
                );

                pots.Add(potView);
            }

            return new ViewDeviationAndTaskResponse(pots);
        }
    }
}