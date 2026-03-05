namespace RUSAL.MetalTapping.BLL.DTOs
{
    public class ReglamentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateStop { get; set; }
    }
}
