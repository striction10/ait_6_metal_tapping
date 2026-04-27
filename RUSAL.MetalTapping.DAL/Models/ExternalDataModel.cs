namespace RUSAL.MetalTapping.DAL.Models;

public class ExternalData
{
    public Guid Id { get; set; }
    public Guid PotId { get; set; }
    public Guid PotParametersGroupId { get; set; }
    public DateTime DateOfReceipt { get; set; }
    public Pot Pot { get; set; } = null!;
    public PotParametersGroup Parameters { get; set; } = null!;
}