namespace RUSAL.MetalTapping.DAL.Models
{
    public class MetalMark
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Order>  Orders { get; set; } = new List<Order>();
    }
}