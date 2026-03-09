namespace RUSAL.MetalTapping.DAL.Models
{
    public class CalculatedTask
    {
        public Guid Id { get; set; }
        public Guid PotId { get; set; }
        public decimal? CalculatedTaskForPot { get; set; }
        public decimal? RoundCalculatedTaskForPot { get; set; }
        public DateTime CreatedAt { get; set; }
        public Pot Pot { get; set; }
    }
}