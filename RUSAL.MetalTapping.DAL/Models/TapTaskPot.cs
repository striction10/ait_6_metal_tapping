namespace RUSAL.MetalTapping.DAL.Models
{
    public class TapTaskPot
    {
        public int Id { get; set; }
        public int TapTaskId { get; set; }
        public int PotId { get; set; }
        public int MetalMarkAnalysisId { get; set; }
        public double PotMetalWeigth { get; set; }
        public TapTask TapTask { get; set; } = null!;
        public Pot Pot { get; set; } = null!;
        public MetalMarkAnalysis Analysis { get; set; } = null!;
    }
}