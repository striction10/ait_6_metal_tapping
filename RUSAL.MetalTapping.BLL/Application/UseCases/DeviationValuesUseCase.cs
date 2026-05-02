using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор получения таблицы регламентов с отклонениями и коэффициентами выливки.
/// </summary>
/// <param name="buildingService">Сервис для работы с корпусами.</param>
/// <param name="reglamentService">Сервис для работы с регламентами.</param>
/// <param name="potReglamentService">Сервис для работы со связями ковшей и регламентов.</param>
/// <param name="potService">Сервис для работы с ковшами.</param>
public class DeviationValuesUseCase(
    BuildingService buildingService,
    ReglamentService reglamentService,
    PotReglamentService potReglamentService,
    PotService potService)
{
    private readonly BuildingService buildingService = buildingService;
    private readonly ReglamentService reglamentService = reglamentService;
    private readonly PotReglamentService potReglamentService = potReglamentService;
    private readonly PotService potService = potService;

    /// <summary>
    /// Создание ViewModel таблицы регламентов для клиента. 
    /// </summary>
    /// <param name="model"></param>
    /// <returns> ViewModel таблицы регламентов. </returns>
    public async Task<ReglamentTableResponse> GetReglamentTableAsync(ReglamentTableRequest model)
    {
        var building = await buildingService.GetByIdAsync(model.buildingId);

        var reglament = await reglamentService.GetByIdAsync(model.reglamentId);

        var potReglaments = await potReglamentService.GetByReglamentAndBuildingIdAsync(reglament.Id, building.Id);

        var pots = new List<PotDeviationDto>();

        foreach (var potReglament in potReglaments)
        {
            var deviation = potReglament.Deviations.FirstOrDefault();
            if (deviation == null)
            {
                continue;
            }

            var pot = await potService.GetByIdAsync(potReglament.PotId);

            var castingRatios = deviation.Values
                .ToDictionary(v => v.Value, v => v.CastingRatio);

            pots.Add(new PotDeviationDto
            {
                Id = potReglament.PotId,
                Name = pot.Name,
                CastingRatio = castingRatios,
            });
        }

        return new ReglamentTableResponse(pots);
    }
}
