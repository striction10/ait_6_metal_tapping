using RUSAL.MetalTapping.BLL.Application.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class BuildingMetalInfoService
{
    /// <summary>
    /// Получение информации о заданном корпусе по заданной марке (сколько металла можно получить в разных группах)
    /// </summary>
    /// <param name="building"> Корпус </param>
    /// <param name="metalMarkId"> Идентификатор марки металла </param>
    /// <returns></returns>
    public BuildingMetalInfo AnalyzeBuilding(BuildingDto building, Guid metalMarkId)
    {
        var groupInfos = new List<PotGroupDto>();

        foreach (var group in building.Groups)
        {
            var pots = group.Pots
                .Where(p => p.MetalMarkId == metalMarkId)
                .ToList();

            if (!pots.Any())
                continue;

            var groupMetalWeight = pots.Sum(p => p.MetalLevel);

            var groupInfo = new PotGroupDto
            {
                Id = group.Id,
                Scoop = group.Scoop,
                Pots = pots,
                GroupMetalWeight = groupMetalWeight
            };

            groupInfos.Add(groupInfo);
        }

        return new BuildingMetalInfo
        {
            BuildingId = building.Id,
            MetalMarkId = metalMarkId,
            Groups = groupInfos,
            TotalMetalWeight = groupInfos.Sum(g => g.GroupMetalWeight)
        };
    }
}