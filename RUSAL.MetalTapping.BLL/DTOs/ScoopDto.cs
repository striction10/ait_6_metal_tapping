namespace RUSAL.MetalTapping.BLL.DTOs
{
    public class ScoopDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BuildingId { get; set; }
        public int StateId { get; set; }
    }
}
