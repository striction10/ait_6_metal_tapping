namespace RUSAL.MetalTapping.BLL.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public double WeightOfMetal { get; set; }
        public int MetalmarkId { get; set; }
        DateTime DateOfOrder { get; set; }
    }
}
