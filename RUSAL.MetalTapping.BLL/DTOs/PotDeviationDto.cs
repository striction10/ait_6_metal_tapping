namespace RUSAL.MetalTapping.BLL.DTOs
{
    public record PotDeviationDto(
        Guid id,
        string name,
        Dictionary<int, int> castingRatio);
}