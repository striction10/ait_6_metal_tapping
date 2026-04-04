using RUSAL.MetalTapping.BLL.Application.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class BuildingMetalInfoService
    {
        public BuildingMetalInfo AnalyzeBuilding(BuildingDto building, Guid metalMarkId)
        {
            var pots = building.Groups
                .SelectMany(g => g.Pots)
                .Where(p => p.MetalMarkId == metalMarkId)
                .ToList();

            return new BuildingMetalInfo
            {
                BuildingId = building.Id,
                MetalMarkId = metalMarkId,
                PotsCount = pots.Count,
                TotalMetalWeight = pots.Sum(p => p.MetalLevel),
                Pots = pots
            };
        }
    }
}