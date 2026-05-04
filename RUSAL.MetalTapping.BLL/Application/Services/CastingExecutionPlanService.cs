using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис расчёта оптимального плана выливки металла.
/// </summary>
/// <param name="groupSelector"> Сервис выбора групп электролизёров. </param>
/// <param name="buildingSelector"> Сервис выбора электролизёров из всех корпусов. </param>
public class CastingExecutionPlanService(
    CastingGroupSelectorService groupSelector,
    CastingBuildingSelectorService buildingSelector)
{
    /// <summary>
    /// Расчёт оптимального маршрута выливки.
    /// </summary>
    /// <param name="buildings"> Корпусы. </param>
    /// <param name="requiredWeight"> Заданное количество металла. </param>
    /// <returns> План выливки. </returns>
    /// <exception cref="BusinessException"> Все ковши заняты - выливка невозможна на данный момент. </exception>
    public ExecutionPlanViewModel SelectExecutionPlan(
        List<BuildingMetalInfoViewModel> buildings,
        double requiredWeight)
    {
        foreach (var b in buildings)
        {
            var g = groupSelector.SelectSingleGroup(b, requiredWeight);
            if (g == null)
            {
                continue;
            }

            return new ExecutionPlanViewModel
            {
                Segments = new List<ExecutionSegmentViewModel>
                {
                    new ExecutionSegmentViewModel
                    {
                        BuildingId = b.BuildingId,
                        GroupId = g.Id,
                        ScoopId = g.Scoop.Id,
                        PotIds = g.Pots.Select(p => p.Id).ToList(),
                        MetalWeight = g.GroupMetalWeight,
                    },
                },
            };
        }

        foreach (var b in buildings)
        {
            var groups = groupSelector.SelectMultiGroupInBuilding(b, requiredWeight);

            if (groups == null || !groups.Any())
            {
                continue;
            }

            var total = groups.SelectMany(x => x.pots).Sum(p => p.MetalLevel);

            if (total < requiredWeight)
            {
                continue;
            }

            return new ExecutionPlanViewModel
            {
                Segments = groups.Select(g => new ExecutionSegmentViewModel
                {
                    BuildingId = b.BuildingId,
                    GroupId = g.group.Id,
                    ScoopId = g.group.Scoop.Id,
                    PotIds = g.pots.Select(p => p.Id).ToList(),
                    MetalWeight = g.pots.Sum(p => p.MetalLevel),
                }).ToList(),
            };
        }

        var multi = buildingSelector.SelectGlobalPots(buildings, requiredWeight);

        if (multi == null)
        {
            throw new BusinessException("Невозможно выполнить заказ ни одним набором групп");
        }

        return new ExecutionPlanViewModel
        {
            Segments = multi.SelectMany(x => x.groups.Select(g => new ExecutionSegmentViewModel
            {
                BuildingId = x.building.BuildingId,
                GroupId = g.group.Id,
                ScoopId = g.group.Scoop.Id,
                PotIds = g.pots.Select(p => p.Id).ToList(),
                MetalWeight = g.pots.Sum(p => p.MetalLevel),
            })).ToList(),
        };
    }
}
