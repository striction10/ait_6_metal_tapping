namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/ShiftTaskDto.cs
public class ShiftTaskDto
=======
public class ShiftTaskDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/ShiftTask.cs
{
    public Guid Id { get; set; }
    public Guid TapTaskId { get; set; }
    public Guid ShiftId { get; set; }
    public DateTime LeadTime { get; set; }
}