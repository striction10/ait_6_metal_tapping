namespace RUSAL.MetalTapping.DAL.Models
{
    public class MetalMarkAnalysisValue
    {
        public Guid Id { get; set; }
        public Guid ChemicalElemId { get; set; }
        public Guid MetalMarkAnalysisId { get; set; }
        public double Value { get; set; }
        public MetalMarkAnalysis Analysis { get; set; } = null!;
        public ChemicalElem ChemicalElem { get; set; } = null!;
    }
}