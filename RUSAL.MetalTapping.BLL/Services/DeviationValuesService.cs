using RUSAL.MetalTapping.BLL.Contracts;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.BLL.Exceptions;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.BLL.Services
{
    public class DeviationValuesService
    {
        private readonly IGenericRepository<Building> _buildingRepository;
        private readonly IReglamentRepository _reglamentRepository;
        private readonly IPotReglamentRepository _potReglamentRepository;

        public DeviationValuesService(
            IGenericRepository<Building> buildingRepository,
            IReglamentRepository reglamentRepository,
            IPotReglamentRepository potReglamentRepository)
        {
            _buildingRepository = buildingRepository;
            _reglamentRepository = reglamentRepository;
            _potReglamentRepository = potReglamentRepository;
        }

        public async Task<ReglamentTableResponse> GetReglamentTableAsync(ReglamentTableRequest model)
        {
            EnsureFound(await _buildingRepository.FindByIdAsync(model.buildingId),
                $"Building with id {model.buildingId} was not found");

            EnsureFound(await _reglamentRepository.FindByIdAsync(model.reglamentId),
                $"Reglament with id {model.reglamentId} was not found");

            var potReglaments =
                await _potReglamentRepository.GetByReglamentAndBuildingWithDeviationsAsync(
                    model.reglamentId, model.buildingId);

            var pots = new List<PotDeviationDto>();

            foreach (var potReglament in potReglaments)
            {
                var deviation = potReglament.Deviations.FirstOrDefault();
                if (deviation == null)
                    continue;

                var castingRatios = new Dictionary<int, int>();

                if (deviation.DeviationValues != null)
                {
                    foreach (var devValue in deviation.DeviationValues)
                    {
                        castingRatios[devValue.Value] = devValue.CastingRatio;
                    }
                }

                pots.Add(new PotDeviationDto(
                    id: potReglament.PotId,
                    name: potReglament.Pot.Name,
                    castingRatio: castingRatios
                ));
            }

            return new ReglamentTableResponse(pots);
        }

        private static T EnsureFound<T>(T entity, string message) // TODO: Вынести в отдельный класс
        {
            if (entity == null)
                throw new NotFoundException(message);

            return entity;
        }
    }

}