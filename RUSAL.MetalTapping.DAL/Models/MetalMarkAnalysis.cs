namespace RUSAL.MetalTapping.DAL.Models
{
    public class MetalMarkAnalysis
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<MetalMarkAnalysisValue> Values { get; set; } = new List<MetalMarkAnalysisValue>();
        public ICollection<TapTaskPot> TapTaskPots { get; set; } = new List<TapTaskPot>();
    }
}