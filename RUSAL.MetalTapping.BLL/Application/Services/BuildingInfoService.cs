using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис сбора информации о корпусе.
/// </summary>
/// <param name="buildingService">Сервис работы с корпусами.</param>
/// <param name="potGroupService">Сервис работы с группами электролизёров.</param>
/// <param name="potService">Сервис работы с электролизёрами.</param>
/// <param name="scoopService">Сервис работы с ковшами.</param>
public class BuildingInfoService(
    BuildingService buildingService,
    PotGroupService potGroupService,
    PotService potService,
    ScoopService scoopService,
    CalculatedTaskService calculatedTaskService,
    MetalMarkAnalysisService metalMarkAnalysisService,
    ScoopUsageService scoopUsageService,
    ScoopStateService scoopStateService)
{
    /// <summary>
    /// Получение информации о корпусе по идентификатору.
    /// </summary>
    /// <param name="buildingId">Идентификатор корпуса.</param>
    /// <returns>ViewModel корпуса.</returns>
    public async Task<BuildingViewModel> GetBuilidngInfo(Guid buildingId)
    {
        var building = await buildingService.GetByIdAsync(buildingId);

        var groups = await potGroupService.GetByBuildingIdAsync(buildingId);
        var groupDtos = new List<PotGroupViewModel>();

        var marksByPot = new Dictionary<Guid, MetalMarkAnalysisDto>();
        var metalLevelByPot = new Dictionary<Guid, double>();

        foreach (var group in groups)
        {
            var scoop = await scoopService.GetByGroupIdAsync(group.Id);
            var scoopUsage = await scoopUsageService.GetByScoopIdAsync(scoop.Id);
            var scoopState = await scoopStateService.GetByIdAsync(scoop.StateId);
            var pots = await potService.GetByGroupIdAsync(group.Id);
            var potIds = pots.Select(p => p.Id).ToList();

            var calculated = await calculatedTaskService.GetByPotIdsAsync(potIds);

            var analysis = await metalMarkAnalysisService.GetByPotIdsAsync(potIds);

            foreach (var a in analysis)
            {
                marksByPot[a.PotId] = a;
            }

            foreach (var c in calculated)
            {
                var weight = c.RoundCalculatedTaskForPot ?? c.CalculatedTaskForPot;
                metalLevelByPot[c.PotId] = weight is double d ? d : (double)c.CalculatedTaskForPot;
            }

            var potDtos = await potService.CreateAsync(pots, calculated, analysis, marksByPot, metalLevelByPot);
            var groupDto = potGroupService.Create(group, scoop, scoopState, scoopUsage, potDtos);
            groupDtos.Add(groupDto);
        }

        return new BuildingViewModel
        {
            Id = building.Id,
            Name = building.Name,
            Groups = groupDtos,
        };
    }
}
