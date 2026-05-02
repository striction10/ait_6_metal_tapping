namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class DeviationDto
{
    public Guid Id { get; set; }
    public double? ActualMetalLevel { get; set; }
    public double TargetMetalLevel { get; set; }
    public Guid PotReglamentId { get; set; }
    public bool? IsValid { get; set; }
    public List<DeviationValuesDto> Values { get; set; } = new();
}
