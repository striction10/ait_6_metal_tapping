namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/ScoopUsageDto.cs
public class ScoopUsageDto
=======
public class ScoopUsageDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/ScoopUsage.cs
{
    public Guid Id { get; set; }
    public Guid ScoopId { get; set; }
    public DateTime BusyFrom { get; set; }
    public DateTime BusyUntil { get; set; }
}
