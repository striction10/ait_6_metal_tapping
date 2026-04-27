namespace RUSAL.MetalTapping.DAL.Models;

public class MetalMarkAnalysisValue
{
    public Guid Id { get; set; }
    public Guid ChemicalElemId { get; set; }
    public Guid MetalMarkAnalysisId { get; set; }
    public double Value { get; set; }
    public MetalMarkAnalysisModel Analysis { get; set; } = null!;
    public ChemicalElemModel ChemicalElem { get; set; } = null!;
}