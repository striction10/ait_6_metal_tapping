namespace RUSAL.MetalTapping.DAL.Models
{
    public class PotReglamentModel
    {
        public Guid Id { get; set; }
        public Guid ReglamentId { get; set; }
        public Guid PotId { get; set; }
        public ReglamentModel Reglament { get; set; } = null!;
        public PotModel Pot { get; set; } = null!;
        public ICollection<DeviationModel> Deviations { get; set; } = new List<DeviationModel>();
    }
}