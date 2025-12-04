namespace RUSAL.MetalTapping.DAL.Entities
{
    public class TapTaskPot
    {
        public int Id { get; set; }
        public int TapTaskId { get; set; }
        public int PotId { get; set; }
        public int MetalMarkAnalysisId { get; set; }
        public double PotMetalWeigth {  get; set; }
    }
}
