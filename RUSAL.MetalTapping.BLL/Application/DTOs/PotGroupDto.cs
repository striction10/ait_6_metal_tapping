using System.Runtime;

namespace RUSAL.MetalTapping.BLL.Application.DTOs
{
    public class PotGroupDto
    {
        public Guid Id { get; set; }
        public double GroupMetalWeight { get; set; }
        public ScoopDto Scoop { get; set; }
        public List<PotDto> Pots { get; set; }
    }
}