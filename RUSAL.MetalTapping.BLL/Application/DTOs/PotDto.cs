namespace RUSAL.MetalTapping.BLL.Application.DTOs
{
    public class PotDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double MetalLevel { get; set; }
        public Guid MetalMarkId { get; set; }
        public string State { get; set; }
    }
}
