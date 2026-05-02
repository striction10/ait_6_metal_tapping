using RUSAL.MetalTapping.BLL.Application.ViewModels;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис выбора электролизёров внутри группы для выполнения заказа на выливку.
/// </summary>
public class CastingPotsService
{
    /// <summary>
    /// Выбор электролизёров внутри группы, которые смогут выполнить выливку заданное количество металла.
    /// </summary>
    /// <param name="group"> Группа электролизёров. </param>
    /// <param name="requiredWeight"> Заданное количество металла. </param>
    /// <returns> Список электролизёров внутри группы, которые смогут выполнить выливку заданного количества металла. </returns>
    public List<PotViewModel>? SelectPotsInGroup(PotGroupViewModel group, double requiredWeight)
    {
        var pots = group.Pots
            .Where(p => p.State == "Активен")
            .OrderByDescending(p => p.MetalLevel)
            .ToList();

        var result = new List<PotViewModel>();
        double sum = 0;

        foreach (var p in pots)
        {
            result.Add(p);
            sum += p.MetalLevel;

            if (sum >= requiredWeight)
            {
                return result;
            }
        }

        return result.Any() ? result : null;
    }
}
