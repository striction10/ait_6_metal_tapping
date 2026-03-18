namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public record PotDeviation(
        Guid id,
        string name,
        Dictionary<int, int> castingRatio);
}