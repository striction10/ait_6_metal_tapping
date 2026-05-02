using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class TapTaskPot : IEntity
{
    public Guid Id { get; set; }
    public Guid TapTaskId { get; set; }
    public Guid PotId { get; set; }
    public Guid MetalMarkAnalysisId { get; set; }
    public double PotMetalWeigth { get; set; }
    public TapTask TapTask { get; set; } = null!;
    public Pot Pot { get; set; } = null!;
    public MetalMarkAnalysis Analysis { get; set; } = null!;
}
