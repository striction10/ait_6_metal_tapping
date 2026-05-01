using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class DeviationValuesUseCase(
    BuildingService buildingService,
    ReglamentService reglamentService,
    PotReglamentService potReglamentService,
    PotService potService)
{
    private readonly BuildingService _buildingService = buildingService;
    private readonly ReglamentService _reglamentService = reglamentService;
    private readonly PotReglamentService _potReglamentService = potReglamentService;
    private readonly PotService _potService = potService;

    /// <summary>
    /// Создание ViewModel таблицы регламентов для клиента 
    /// </summary>
    /// <param name="model"></param>
    /// <returns> ViewModel таблицы регламентов </returns>
    public async Task<ReglamentTableResponse> GetReglamentTableAsync(ReglamentTableRequest model)
    {
        var building = await _buildingService.GetByIdAsync(model.buildingId);

        var reglament = await _reglamentService.GetByIdAsync(model.reglamentId);

        var potReglaments = await _potReglamentService.GetByReglamentAndBuildingIdAsync(reglament.Id, building.Id);

        var pots = new List<PotDeviationDto>();

        foreach (var potReglament in potReglaments)
        {
            var deviation = potReglament.Deviations.FirstOrDefault();
            if (deviation == null)
                continue;

            var pot = await _potService.GetByIdAsync(potReglament.PotId);

            var castingRatios = deviation.Values
                .ToDictionary(v => v.Value, v => v.CastingRatio);

            pots.Add(new PotDeviationDto
            {
                Id = potReglament.PotId,
                Name = pot.Name,
                CastingRatio = castingRatios
            });
        }

        return new ReglamentTableResponse(pots);
    }
}