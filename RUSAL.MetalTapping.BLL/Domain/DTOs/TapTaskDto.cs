namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/TapTaskDto.cs
public class TapTaskDto
=======
public class TapTaskDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/TapTask.cs
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ScoopId { get; set; }
}