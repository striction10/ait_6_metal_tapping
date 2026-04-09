using RUSAL.MetalTapping.BLL.Application.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class CastingGroupSelectorService
    {
        private readonly CastingPotsService _potsSelector;

        public CastingGroupSelectorService(CastingPotsService potsSelector)
        {
            _potsSelector = potsSelector;
        }

        private bool IsAvailable(PotGroupDto group)
        {
            if (group.Scoop.State != "Активен")
                return false;

            if (group.Scoop.IsBusy)
                return false;

            if (group.Pots.Any(p => p.State != "Активен"))
                return false;

            return true;
        }

        public PotGroupDto? SelectSingleGroup(BuildingMetalInfo building, double requiredWeight)
        {
            return building.Groups
                .Where(g => IsAvailable(g) && g.GroupMetalWeight >= requiredWeight)
                .OrderBy(g => g.GroupMetalWeight - requiredWeight)
                .FirstOrDefault();
        }

        public List<(PotGroupDto group, List<PotDto> pots)> SelectMultiGroupInBuilding(
            BuildingMetalInfo building, 
            double requiredWeight)
        {
            var allPots = building.Groups
                .Where(g => IsAvailable(g))
                .SelectMany(g => g.Pots
                    .Where(p => p.State == "Активен")
                    .Select(p => (group: g, pot: p)))
                .OrderByDescending(x => x.pot.MetalLevel)
                .ToList();

            var result = new Dictionary<PotGroupDto, List<PotDto>>();
            double sum = 0;

            foreach (var (group, pot) in allPots)
            {
                if (!result.ContainsKey(group))
                    result[group] = new List<PotDto>();

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
}
