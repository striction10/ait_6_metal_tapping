using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class GroupService
    {
        public PotGroupDto Create(
            PotGroup group,
            Scoop scoop,
            ScoopState scoopState,
            IEnumerable<ScoopUsage> scoopUsages,
            List<PotDto> pots)
        {
            var isBusy = scoopUsages.Any(u => u.BusyUntil > DateTime.UtcNow);

            var scoopDto = new ScoopDto
            {
                Id = scoop.Id,
                State = scoopState.Name,
                IsBusy = isBusy
            };

            return new PotGroupDto
            {
                Id = group.Id,
                Scoop = scoopDto,
                Pots = pots
            };
        }
    }
}
