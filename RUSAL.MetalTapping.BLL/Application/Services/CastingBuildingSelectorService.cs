using RUSAL.MetalTapping.BLL.Application.ViewModels;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис выбора электролизёров для выливки из всех доступных корпусов.
/// </summary>
public class CastingBuildingSelectorService
{
    /// <summary>
    /// Выбор электролизёров для выливки из всех корпусов.
    /// </summary>
    /// <param name="buildings"> Корпусы. </param>
    /// <param name="requiredWeight"> Заданное количество металла. </param>
    /// <returns> Список электролизёров для задания на выливку. </returns>
    public List<(BuildingMetalInfoViewModel building, List<(PotGroupViewModel group, List<PotViewModel> pots)> groups)>? SelectGlobalPots(
        List<BuildingMetalInfoViewModel> buildings,
        double requiredWeight)
    {
        var allPots = buildings
            .SelectMany(b => b.Groups
                .Where(g => g.Scoop.State == "Активен" && !g.Scoop.IsBusy)
                .SelectMany(g => g.Pots
                    .Where(p => p.State == "Активен")
                    .Select(p => (building: b, group: g, pot: p))))
            .OrderByDescending(x => x.pot.MetalLevel)
            .ToList();

        double sum = 0;

        var selected = new List<(BuildingMetalInfoViewModel building, PotGroupViewModel group, PotViewModel pot)>();

        foreach (var item in allPots)
        {
            selected.Add(item);
            sum += item.pot.MetalLevel;

            if (sum >= requiredWeight)
            {
                break;
            }
        }

        if (sum < requiredWeight)
        {
            return null;
        }

        var result = selected
            .GroupBy(x => x.building)
            .Select(b => (
                building: b.Key,
                groups: b.GroupBy(x => x.group)
                         .Select(g => (
                             group: g.Key,
                             pots: g.Select(x => x.pot).ToList()
                         )).ToList()
            ))
            .ToList();

        return result;
    }
}
