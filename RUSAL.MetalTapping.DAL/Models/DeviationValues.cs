namespace RUSAL.MetalTapping.DAL.Models
{
    public class DeviationValues
    {
        public int Id { get; set; }
        public int DeviationId { get; set; }
        public double Value { get; set; }
        public double CastingRatio { get; set; }
        public Deviation Deviation { get; set; } = null!;
    }
}