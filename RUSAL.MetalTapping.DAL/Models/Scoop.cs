namespace RUSAL.MetalTapping.DAL.Entities
{
    public class Scoop
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BuildingId { get; set; }
        public int StateId { get; set; }
    }
}
