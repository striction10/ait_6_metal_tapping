using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class DeviationValuesUseCase(
    IGenericRepository<BuildingDto> buildingRepository,
    IReglamentRepository reglamentRepository,
    IPotReglamentRepository potReglamentRepository,
    IGenericRepository<PotDto> potRepository)
{
    private readonly IGenericRepository<BuildingDto> _buildingRepository = buildingRepository;
    private readonly IReglamentRepository _reglamentRepository = reglamentRepository;
    private readonly IPotReglamentRepository _potReglamentRepository = potReglamentRepository;
    private readonly IGenericRepository<PotDto> _potRepository = potRepository;

    /// <summary>
    /// Создание ViewModel таблицы регламентов для клиента 
    /// </summary>
    /// <param name="model"></param>
    /// <returns> ViewModel таблицы регламентов </returns>
    public async Task<ReglamentTableResponse> GetReglamentTableAsync(ReglamentTableRequest model)
    {
        EnsureFound(await _buildingRepository.GetByIdAsync(model.buildingId),
            $"Building with id {model.buildingId} was not found");

        EnsureFound(await _reglamentRepository.GetByIdAsync(model.reglamentId),
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

            var pot = await _potRepository.GetByIdAsync(potReglament.PotId);
            EnsureFound(pot, $"Pot with id {potReglament.PotId} was not found");

            var castingRatios = deviation.Values
                .ToDictionary(v => v.Value, v => v.CastingRatio);

            pots.Add(new PotDeviationDto(
                id: potReglament.PotId,
                name: pot.Name,
                castingRatio: castingRatios
            ));
        }

        return new ReglamentTableResponse(pots);
    }
}