namespace RUSAL.MetalTapping.DAL.Models
{
    public class ScoopState
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Scoop> Scoops = new List<Scoop>();
    }
}