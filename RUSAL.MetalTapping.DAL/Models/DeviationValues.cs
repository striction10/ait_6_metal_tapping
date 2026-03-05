namespace RUSAL.MetalTapping.DAL.Models
{
    public class DeviationValues
    {
        public Guid Id { get; set; }
        public Guid DeviationId { get; set; }
        public int Value { get; set; }
        public int CastingRatio { get; set; }
        public Deviation Deviation { get; set; } = null!;
    }
}