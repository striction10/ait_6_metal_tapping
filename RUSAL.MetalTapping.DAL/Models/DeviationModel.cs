namespace RUSAL.MetalTapping.DAL.Models;

public class DeviationModel
{
    public Guid Id { get; set; }
    public double? ActualMetalLevel { get; set; }
    public double TargetMetalLevel { get; set; }
    public Guid PotReglamentId { get; set; }
    public bool? IsValid { get; set; }
    public PotReglamentModel PotReglament { get; set; } = null!;
    public ICollection<DeviationValuesModel>  DeviationValues { get; set; } = new List<DeviationValuesModel>();
}