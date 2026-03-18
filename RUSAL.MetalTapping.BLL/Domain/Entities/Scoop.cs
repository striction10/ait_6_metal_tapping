namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class Scoop
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid BuildingId { get; set; }
        public Guid StateId { get; set; }
    }
}