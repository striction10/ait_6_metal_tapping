using RUSAL.MetalTapping.BLL.Application.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class CastingPotsService
{
    /// <summary>
    /// Выбор электролизёров внутри группы, которые смогут выполнить выливку заданное количество металла
    /// </summary>
    /// <param name="group"> Группа электролизёров </param>
    /// <param name="requiredWeight"> Заданное количество металла </param>
    /// <returns> Список электролизёров внутри группы, которые смогут выполнить выливку заданного количества металла </returns>
    public List<PotDto>? SelectPotsInGroup(PotGroupDto group, double requiredWeight)
    {
        var pots = group.Pots
            .Where(p => p.State == "Активен")
            .OrderByDescending(p => p.MetalLevel)
            .ToList();

        var result = new List<PotDto>();
        double sum = 0;

        foreach (var p in pots)
        {
            result.Add(p);
            sum += p.MetalLevel;

            if (sum >= requiredWeight)
                return result;
        }

        return result.Any() ? result : null;
    }
}