using RUSAL.MetalTapping.BLL.Contracts;
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

        public async Task GetReglamentTableAsync(ReglamentTableRequest model)
        {
            var existingBuilding = await _buildingRepository.FindByIdAsync(model.buildingId);

            if (existingBuilding == null)
            {
                throw new NotFoundException($"Building with id {model.buildingId} was not found");
            }

            if (model.reglamentId.HasValue)
            {
                var existingReglament = await _reglamentRepository.FindByIdAsync(model.reglamentId.Value);

                if (existingReglament == null)
                {
                    throw new NotFoundException($"Reglament with id {model.reglamentId} was not found");
                }

                var existingPotReglaments = _potReglamentRepository.getByReglamentId(model.reglamentId.Value);
            }
            else
            {
                var latestReglament = await _reglamentRepository.GetNewReglament();
                var existingPotReglaments = _potReglamentRepository.getByReglamentId(latestReglament.Id);
            }

            //TODO: Фильтрация по корпусам
        }
    }
}
