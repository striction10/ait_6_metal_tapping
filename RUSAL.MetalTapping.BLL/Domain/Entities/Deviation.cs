using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class DeviationDto : IDomain
{
    public Guid Id { get; set; }
    public double? ActualMetalLevel { get; set; }
    public double TargetMetalLevel { get; set; }
    public Guid PotReglamentId { get; set; }
    public bool? IsValid { get; set; }
    public List<DeviationValuesDto> Values { get; set; } = new();
}