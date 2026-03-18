namespace RUSAL.MetalTapping.DAL.Models
{
    public class PotParameterModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public Guid PotParametersGroupId { get; set; }
        public PotParametersGroupModel Group { get; set; } = null!;
    }
}