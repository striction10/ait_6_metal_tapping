using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.UseCases
{
    public class DeviationValuesUseCase
    {
        private readonly IGenericRepository<Building> _buildingRepository;
        private readonly IReglamentRepository _reglamentRepository;
        private readonly IPotReglamentRepository _potReglamentRepository;
        private readonly IGenericRepository<Pot> _potRepository;

        public DeviationValuesUseCase(
            IGenericRepository<Building> buildingRepository,
            IReglamentRepository reglamentRepository,
            IPotReglamentRepository potReglamentRepository,
            IGenericRepository<Pot> potRepository)
        {
            _buildingRepository = buildingRepository;
            _reglamentRepository = reglamentRepository;
            _potReglamentRepository = potReglamentRepository;
            _potRepository = potRepository;
        }

        public async Task<ReglamentTableResponse> GetReglamentTableAsync(ReglamentTableRequest model)
        {
            EnsureFound(await _buildingRepository.GetByIdAsync(model.buildingId),
                $"Building with id {model.buildingId} was not found");

            EnsureFound(await _reglamentRepository.GetByIdAsync(model.reglamentId),
                $"Reglament with id {model.reglamentId} was not found");

            var potReglaments =
                await _potReglamentRepository.GetByReglamentAndBuildingWithDeviationsAsync(
                    model.reglamentId, model.buildingId);

            var pots = new List<PotDeviation>();

            foreach (var potReglament in potReglaments)
            {
                var deviation = potReglament.Deviations.FirstOrDefault();
                if (deviation == null)
                    continue;

                var pot = await _potRepository.GetByIdAsync(potReglament.PotId);
                EnsureFound(pot, $"Pot with id {potReglament.PotId} was not found");

                var castingRatios = deviation.Values
                    .ToDictionary(v => v.Value, v => v.CastingRatio);

                pots.Add(new PotDeviation(
                    id: potReglament.PotId,
                    name: pot.Name,
                    castingRatio: castingRatios
                ));
            }

            return new ReglamentTableResponse(pots);
        }
    }
}