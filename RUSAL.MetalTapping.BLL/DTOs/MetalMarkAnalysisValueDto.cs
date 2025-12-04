namespace RUSAL.MetalTapping.BLL.DTOs
{
    public class MetalMarkAnalysisValueDto
    {
        public int Id { get; set; }
        public int ChemicalElemId { get; set; }
        public double Value { get; set; }
        public DateTime DateOfReceipt { get; set; }
    }
}
