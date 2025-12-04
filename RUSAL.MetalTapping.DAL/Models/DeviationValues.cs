using Microsoft.EntityFrameworkCore;

namespace RUSAL.MetalTapping.DAL.Entities
{
    [Keyless]
    public class DeviationValues
    {
        public int DeviationId { get; set; }
        public double Value { get; set; }
        public double CastingRatio { get; set; }
    }
}
