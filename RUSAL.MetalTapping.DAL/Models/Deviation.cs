namespace RUSAL.MetalTapping.DAL.Entities
{
    public class Deviation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double TargetMetalLevel { get; set; }
        public int PotReglamentId { get; set; }
        public PotReglament PotReglament { get; set; } = null!;
    }
}
