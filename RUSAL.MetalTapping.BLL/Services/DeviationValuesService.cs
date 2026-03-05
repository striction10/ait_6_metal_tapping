using System.Linq;
using System.Net.WebSockets;
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

            var existingPotsDeviations = await _potReglamentRepository.GetByReglamentAndBuildingWithDeviationsAsync(model.reglamentId, model.buildingId);

            var pots = new List<PotDeviationDto>();

            foreach (var potReglament in existingPotsDeviations)
            {
                var deviation = potReglament.Deviations.FirstOrDefault();

                var castingRatios = new Dictionary<int, int>();

                if (deviation?.DeviationValues != null)
                {
                    foreach (var devValue in deviation.DeviationValues)
                    {
                        castingRatios[devValue.Value] = devValue.CastingRatio;
                    }
                }

                var potDevDto = new PotDeviationDto
                    (
                        id: potReglament.PotId,
                        name: deviation?.Name,
                        castingRatio: castingRatios
                    );

                pots.Add(potDevDto);
            }

            var response = new ReglamentTableResponse
                (
                    building: new BuildingDto
                    {
                        Id = existingBuilding.Id,
                        Name = existingBuilding.Name
                    },
                    reglament: new ReglamentDto
                    {
                        Id = existingReglament.Id,
                        Name = existingReglament.Name,
                        DateStart = existingReglament.DateStart,
                        DateStop = existingReglament.DateStop
                    },
                    pots: pots
                );

            return response;
        }
    }
}