namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }
    public double WeightOfMetal { get; set; }
    public Guid MetalmarkId { get; set; }
    public DateTime DateOfOrder { get; set; }
    public decimal RemainingWeight { get; set; }
    public int Status { get; set; }
}
