namespace RUSAL.MetalTapping.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double WeightOfMetal { get; set; }
        public int MetalMarkId { get; set; }
        public DateTime DateOfOrder { get; set; }
        public MetalMark MetalMark { get; set; } = null!;
        public ICollection<TapTask> TapTasks { get; set; } = new List<TapTask>();
    }
}