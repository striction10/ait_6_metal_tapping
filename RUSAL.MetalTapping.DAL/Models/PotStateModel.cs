namespace RUSAL.MetalTapping.DAL.Models
{
    public class PotStateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PotModel> Pots = new List<PotModel>();
    }
}