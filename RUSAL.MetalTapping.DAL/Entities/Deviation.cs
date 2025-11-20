namespace RUSAL.MetalTapping.DAL.Entities
{
    public class Deviation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TargetMetalLevel { get; set; }
        public int PotReglamentId { get; set; }
    }
}
