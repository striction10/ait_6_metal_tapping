using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class MetalMarkService
    {
        public string ResolveMetalMarkName(MetalMark? metalMark)
            => metalMark?.Name ?? "N/A";
    }
}