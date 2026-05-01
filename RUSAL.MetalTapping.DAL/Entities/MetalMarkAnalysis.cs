using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class MetalMarkAnalysis : IEntity
{
    public Guid Id { get; set; }
    public Guid PotId { get; set; }
    public Guid MetalMarkId { get; set; }
    public DateTime DateOfReceipt { get; set; }
    public Pot Pot { get; set; } = null!;
    public MetalMark MetalMark { get; set; } = null!;
    public ICollection<MetalMarkAnalysisValue> Values { get; set; } = new List<MetalMarkAnalysisValue>();
    public ICollection<TapTaskPot> TapTaskPots { get; set; } = new List<TapTaskPot>();
}