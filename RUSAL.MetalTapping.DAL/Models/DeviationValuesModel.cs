namespace RUSAL.MetalTapping.DAL.Models;

public class DeviationValuesModel
{
    public Guid Id { get; set; }
    public Guid DeviationId { get; set; }
    public int Value { get; set; }
    public int CastingRatio { get; set; }
    public DeviationModel Deviation { get; set; } = null!;
}