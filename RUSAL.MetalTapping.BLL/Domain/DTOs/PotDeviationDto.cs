namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class PotDeviationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<int, int> CastingRatio { get; set; }
}
