namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class MetalMarkAnalysisDto
{
    public Guid Id { get; set; }
    public Guid PotId { get; set; }
    public Guid MetalMarkId { get; set; }
    public DateTime DateOfReceipt { get; set; }
    public List<MetalMarkAnalysisValueDto> Values { get; set; } = new();
}
