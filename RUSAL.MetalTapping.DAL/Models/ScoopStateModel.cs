namespace RUSAL.MetalTapping.DAL.Models
{
    public class ScoopStateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ScoopModel> Scoops = new List<ScoopModel>();
    }
}