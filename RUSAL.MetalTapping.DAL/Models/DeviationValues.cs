namespace RUSAL.MetalTapping.DAL.Models
{
    public class DeviationValues
    {
        public Guid Id { get; set; }
        public Guid DeviationId { get; set; }
        public double Value { get; set; }
        public double CastingRatio { get; set; }
        public Deviation Deviation { get; set; } = null!;
    }
}