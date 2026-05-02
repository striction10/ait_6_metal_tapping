using RUSAL.MetalTapping.BLL.Application.ViewModels;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис анализа информации о корпусе по заданной марке металла.
/// </summary>
public class BuildingMetalInfoService
{
    /// <summary>
    /// Получение информации о заданном корпусе по заданной марке (сколько металла можно получить в разных группах).
    /// </summary>
    /// <param name="building"> Корпус. </param>
    /// <param name="metalMarkId"> Идентификатор марки металла. </param>
    /// <returns> ViewModel информации о корпусе. </returns>
    public BuildingMetalInfoViewModel AnalyzeBuilding(BuildingViewModel building, Guid metalMarkId)
    {
        var groupInfos = new List<PotGroupViewModel>();

        foreach (var group in building.Groups)
        {
            var pots = group.Pots
                .Where(p => p.MetalMarkId == metalMarkId)
                .ToList();

            if (!pots.Any())
            {
                continue;
            }

            var groupMetalWeight = pots.Sum(p => p.MetalLevel);

            var groupInfo = new PotGroupViewModel
            {
                Id = group.Id,
                Scoop = group.Scoop,
                Pots = pots,
                GroupMetalWeight = groupMetalWeight,
            };

            groupInfos.Add(groupInfo);
        }

        return new BuildingMetalInfoViewModel
        {
            BuildingId = building.Id,
            MetalMarkId = metalMarkId,
            Groups = groupInfos,
            TotalMetalWeight = groupInfos.Sum(g => g.GroupMetalWeight),
        };
    }
}
