namespace RUSAL.MetalTapping.DAL.Models;

public class TapTaskPot
{
    public Guid Id { get; set; }
    public Guid TapTaskId { get; set; }
    public Guid PotId { get; set; }
    public Guid MetalMarkAnalysisId { get; set; }
    public double PotMetalWeigth { get; set; }
    public TapTaskModel TapTask { get; set; } = null!;
    public PotModel Pot { get; set; } = null!;
    public MetalMarkAnalysisModel Analysis { get; set; } = null!;
}