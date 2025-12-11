namespace RUSAL.MetalTapping.DAL.Models
{
    public class MetalMarkAnalysisValue
    {
        public int Id { get; set; }
        public int ChemicalElemId { get; set; }
        public int MetalMarkAnalysisId { get; set; }
        public double Value { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public MetalMarkAnalysis Analysis { get; set; } = null!;
        public ChemicalElem ChemicalElem { get; set; } = null!;
    }
}