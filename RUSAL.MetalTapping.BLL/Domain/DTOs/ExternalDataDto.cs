namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class ExternalDataDto
{
    public Guid Id { get; set; }
    public Guid PotId { get; set; }
    public Guid PotParametersGroupId { get; set; }
    public DateTime DateOfReceipt { get; set; }
}