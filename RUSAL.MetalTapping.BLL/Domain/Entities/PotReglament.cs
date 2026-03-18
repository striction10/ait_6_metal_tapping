namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class PotReglament
    {
        public Guid Id { get; set; }
        public Guid ReglamentId { get; set; }
        public Guid PotId { get; set; }
        public List<Deviation> Deviations { get; set; } = new();
    }
}