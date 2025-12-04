namespace RUSAL.MetalTapping.BLL.DTOs
{
    public class TapTaskPotDto
    {
        public int Id { get; set; }
        public int TapTaskId { get; set; }
        public int PotId { get; set; }
        public int MetalMarkAnalysisId { get; set; }
        public double PotMetalWeigth { get; set; }
    }
}
