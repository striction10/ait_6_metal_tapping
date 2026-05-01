using RUSAL.MetalTapping.BLL.Application.ViewModels;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class CastingGroupSelectorService
{
    /// <summary>
    /// Фильтрация группы по статусу активности ковша
    /// </summary>
    /// <param name="group"> Группа электролизёров </param>
    /// <returns> Доступна ли группа для выливки (Свободен ли ковш) </returns>
    private bool IsAvailable(PotGroupViewModel group)
    {
        if (group.Scoop.State != "Активен")
            return false;

        if (group.Scoop.IsBusy)
            return false;

        if (group.Pots.Any(p => p.State != "Активен"))
            return false;

        return true;
    }

    /// <summary>
    /// Выбор единичной группы, которая сможет выполнить выливку заданного количества металла
    /// </summary>
    /// <param name="building"> Корпус </param>
    /// <param name="requiredWeight"> Заданное количество металла </param>
    /// <returns> Группа в корпусе, которая сможет выполнить выливку заданного количества металла </returns>
    public PotGroupViewModel? SelectSingleGroup(BuildingMetalInfoViewModel building, double requiredWeight)
    {
        return building.Groups
            .Where(g => IsAvailable(g) && g.GroupMetalWeight >= requiredWeight)
            .OrderBy(g => g.GroupMetalWeight - requiredWeight)
            .FirstOrDefault();
    }

    /// <summary>
    /// Выбор нескольких групп в одном корпусе, которые смогут выполнить выливку заданного количества металла
    /// </summary>
    /// <param name="building"> Корпус </param>
    /// <param name="requiredWeight"> Заданное количество металла </param>
    /// <returns> Список групп в корпусе, которые смогут выполнить выливку заданного количества металла </returns>
    public List<(PotGroupViewModel group, List<PotViewModel> pots)> SelectMultiGroupInBuilding(
        BuildingMetalInfoViewModel building, 
        double requiredWeight)
    {
        var allPots = building.Groups
            .Where(g => IsAvailable(g))
            .SelectMany(g => g.Pots
                .Where(p => p.State == "Активен")
                .Select(p => (group: g, pot: p)))
            .OrderByDescending(x => x.pot.MetalLevel)
            .ToList();

        var result = new Dictionary<PotGroupViewModel, List<PotViewModel>>();
        double sum = 0;

        foreach (var (group, pot) in allPots)
        {
            if (!result.ContainsKey(group))
                result[group] = new List<PotViewModel>();

            result[group].Add(pot);
            sum += pot.MetalLevel;

            if (sum >= requiredWeight)
                break;
        }

        return result
            .Select(x => (x.Key, x.Value))
            .ToList();
    }
}
