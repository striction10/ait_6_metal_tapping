using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class MetalMarkAnalysisValue : IEntity
{
    public Guid Id { get; set; }
    public Guid ChemicalElemId { get; set; }
    public Guid MetalMarkAnalysisId { get; set; }
    public double Value { get; set; }
    public MetalMarkAnalysis Analysis { get; set; } = null!;
    public ChemicalElem ChemicalElem { get; set; } = null!;
}