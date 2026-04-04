namespace RUSAL.MetalTapping.BLL.Application.DTOs
{
    public class BuildingMetalInfo
    {
        public Guid BuildingId { get; set; }
        public Guid MetalMarkId { get; set; }
        public int PotsCount { get; set; }
        public decimal TotalMetalWeight { get; set; }
        public List<PotDto> Pots { get; set; }
    }
}
