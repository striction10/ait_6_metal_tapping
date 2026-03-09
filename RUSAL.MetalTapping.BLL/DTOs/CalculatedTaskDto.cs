namespace RUSAL.MetalTapping.BLL.DTOs
{
    public class CalculatedTaskDto
    {
        public Guid Id { get; set; }
        public Guid PotId { get; set; }
        public decimal? CalculatedTaskForPot { get; set; }
        public decimal? RoundCalculatedTaskForPot { get; set; }
    }
}
