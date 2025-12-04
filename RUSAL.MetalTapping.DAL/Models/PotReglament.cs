namespace RUSAL.MetalTapping.DAL.Entities
{
    public class PotReglament
    {
        public int Id { get; set; }
        public int ReglamentId { get; set; }
        public int PotId { get; set; }
        public ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();
    }
}