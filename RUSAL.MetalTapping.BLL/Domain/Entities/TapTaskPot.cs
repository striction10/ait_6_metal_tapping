namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class TapTaskPot
    {
        public Guid Id { get; set; }
        public Guid TapTaskId { get; set; }
        public Guid PotId { get; set; }
        public Guid MetalMarkAnalysisId { get; set; }
        public double PotMetalWeigth { get; set; }
    }
}