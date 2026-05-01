namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/ShiftDto.cs
public class ShiftDto
=======
public class ShiftDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/Shift.cs
{
    public Guid Id { get; set; }
    public Guid WorkGroupId { get; set; }
    public Guid BuildingId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
}