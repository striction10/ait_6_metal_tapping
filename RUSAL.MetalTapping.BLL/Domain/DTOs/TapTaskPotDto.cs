namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class TapTaskPotDto
{
    public Guid Id { get; set; }
    public Guid TapTaskId { get; set; }
    public Guid PotId { get; set; }
    public Guid MetalMarkAnalysisId { get; set; }
    public double PotMetalWeigth { get; set; }
}