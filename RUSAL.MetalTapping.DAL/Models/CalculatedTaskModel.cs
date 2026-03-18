namespace RUSAL.MetalTapping.DAL.Models
{
    public class CalculatedTaskModel
    {
        public Guid Id { get; set; }
        public Guid PotId { get; set; }
        public double? CalculatedTaskForPot { get; set; }
        public double? RoundCalculatedTaskForPot { get; set; }
        public DateTime CreatedAt { get; set; }
        public PotModel Pot { get; set; }
    }
}