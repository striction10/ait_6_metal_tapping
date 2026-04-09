using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class CastingExecutionPlanService
    {
        private readonly CastingGroupSelectorService _groupSelector;
        private readonly CastingBuildingSelectorService _buildingSelector;

        public CastingExecutionPlanService(
            CastingGroupSelectorService groupSelector,
            CastingBuildingSelectorService buildingSelector)
        {
            _groupSelector = groupSelector;
            _buildingSelector = buildingSelector;
        }

        public ExecutionPlan SelectExecutionPlan(
            List<BuildingMetalInfo> buildings,
            double requiredWeight)
        {
            foreach (var b in buildings)
            {
                var g = _groupSelector.SelectSingleGroup(b, requiredWeight);
                if (g != null)
                {
                    return new ExecutionPlan
                    {
                        Segments = new List<ExecutionSegment>
                        {
                            new ExecutionSegment
                            {
                                BuildingId = b.BuildingId,
                                GroupId = g.Id,
                                ScoopId = g.Scoop.Id,
                                PotIds = g.Pots.Select(p => p.Id).ToList(),
                                MetalWeight = g.GroupMetalWeight
                            }
                        }
                    };
                }
            }

            foreach (var b in buildings)
            {
                var groups = _groupSelector.SelectMultiGroupInBuilding(b, requiredWeight);

                if (groups != null && groups.Any())
                {
                    var total = groups.SelectMany(x => x.pots).Sum(p => p.MetalLevel);

                    if (total >= requiredWeight)
                    {
                        return new ExecutionPlan
                        {
                            Segments = groups.Select(g => new ExecutionSegment
                            {
                                BuildingId = b.BuildingId,
                                GroupId = g.group.Id,
                                ScoopId = g.group.Scoop.Id,
                                PotIds = g.pots.Select(p => p.Id).ToList(),
                                MetalWeight = g.pots.Sum(p => p.MetalLevel)
                            }).ToList()
                        };
                    }
                }
            }

            var multi = _buildingSelector.SelectGlobalPots(buildings, requiredWeight);

            if (multi != null)
            {
                return new ExecutionPlan
                {
                    Segments = multi.SelectMany(x => x.groups.Select(g => new ExecutionSegment
                        {
                            BuildingId = x.building.BuildingId,
                            GroupId = g.group.Id,
                            ScoopId = g.group.Scoop.Id,
                            PotIds = g.pots.Select(p => p.Id).ToList(),
                            MetalWeight = g.pots.Sum(p => p.MetalLevel)
                        }))
                        .ToList()
                };
            }

            throw new BusinessException("Невозможно выполнить заказ ни одним набором групп");
        }
    }
}