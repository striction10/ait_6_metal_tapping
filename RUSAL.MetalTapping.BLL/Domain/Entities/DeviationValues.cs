namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class DeviationValues
    {
        public Guid Id { get; set; }
        public Guid DeviationId { get; set; }
        public int Value { get; set; }
        public int CastingRatio { get; set; }
    }
}