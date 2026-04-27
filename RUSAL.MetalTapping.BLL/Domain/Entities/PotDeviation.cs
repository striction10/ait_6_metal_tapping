namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public record PotDeviationDto(
    Guid id,
    string name,
    Dictionary<int, int> castingRatio);