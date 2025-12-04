namespace RUSAL.MetalTapping.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public double WeightOfMetal { get; set; }
        public int MetalmarkId { get; set; }
        DateTime DateOfOrder { get; set; }
    }
}
