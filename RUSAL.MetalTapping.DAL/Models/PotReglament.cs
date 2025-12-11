namespace RUSAL.MetalTapping.DAL.Models
{
    public class PotReglament
    {
        public int Id { get; set; }
        public int ReglamentId { get; set; }
        public int PotId { get; set; }
        public Reglament Reglament { get; set; } = null!;
        public Pot Pot { get; set; } = null!;
        public ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();
    }
}