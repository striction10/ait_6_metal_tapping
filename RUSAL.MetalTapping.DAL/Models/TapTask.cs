namespace RUSAL.MetalTapping.DAL.Entities
{
    public class TapTask
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int OrderId { get; set; }
        public int ScoopId { get; set; }
    }
}
