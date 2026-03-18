namespace RUSAL.MetalTapping.DAL.Models
{
    public class PotParametersGroupModel
    {
        public Guid Id { get; set; }

        public ICollection<PotParameterModel> Parameters { get; set; } = new List<PotParameterModel>();
    }
}