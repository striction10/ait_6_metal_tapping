namespace RUSAL.MetalTapping.DAL.Entities
{
    public class Reglament
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateStop { get; set; }
    }
}
