namespace RUSAL.MetalTapping.DAL.Models
{
    public class PotParametersGroup
    {
        public int Id { get; set; }

        public ICollection<PotParameter> Parameters { get; set; } = new List<PotParameter>();
    }
}