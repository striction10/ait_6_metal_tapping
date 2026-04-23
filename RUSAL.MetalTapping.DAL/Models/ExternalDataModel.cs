namespace RUSAL.MetalTapping.DAL.Models;

public class ExternalDataModel
{
    public Guid Id { get; set; }
    public Guid PotId { get; set; }
    public Guid PotParametersGroupId { get; set; }
    public DateTime DateOfReceipt { get; set; }
    public PotModel Pot { get; set; } = null!;
    public PotParametersGroupModel Parameters { get; set; } = null!;
}