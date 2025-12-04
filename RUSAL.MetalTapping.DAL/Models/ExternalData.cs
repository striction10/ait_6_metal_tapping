using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Entities
{
    public class ExternalData
    {
        public int Id { get; set; }
        public int PotId { get; set; }
        public int PotParametersGroupId { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public Pot Pot { get; set; } = null!;
        public PotParametersGroup Parameters { get; set; } = null!;
    }
}
