namespace RUSAL.MetalTapping.DAL.Models
{
    public class Deviation
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double TargetMetalLevel { get; set; }
        public Guid PotReglamentId { get; set; }
        public PotReglament PotReglament { get; set; } = null!;
        public ICollection<DeviationValues>  DeviationValues { get; set; } = new List<DeviationValues>();
    }
}