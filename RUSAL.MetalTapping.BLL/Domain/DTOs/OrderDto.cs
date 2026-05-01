namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/OrderDto.cs
public class OrderDto
=======
public class OrderDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/Order.cs
{
    public Guid Id { get; set; }
    public double WeightOfMetal { get; set; }
    public Guid MetalmarkId { get; set; }
    public DateTime DateOfOrder { get; set; }
}