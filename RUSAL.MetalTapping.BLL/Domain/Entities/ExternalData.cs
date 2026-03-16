namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class ExternalData
    {
        public Guid Id { get; set; }
        public Guid PotId { get; set; }
        public Guid PotParametersGroupId { get; set; }
        public DateTime DateOfReceipt { get; set; }
    }
}