namespace RUSAL.MetalTapping.DAL.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public double WeightOfMetal { get; set; }
        public Guid MetalMarkId { get; set; }
        public DateTime DateOfOrder { get; set; }
        public MetalMark MetalMark { get; set; } = null!;
        public ICollection<TapTask> TapTasks { get; set; } = new List<TapTask>();
    }
}