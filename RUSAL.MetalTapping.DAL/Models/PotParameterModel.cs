namespace RUSAL.MetalTapping.DAL.Models;

public class PotParameter
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public Guid PotParametersGroupId { get; set; }
    public PotParametersGroup Group { get; set; } = null!;
}