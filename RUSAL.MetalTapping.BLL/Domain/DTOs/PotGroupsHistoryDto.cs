namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/PotGroupsHistoryDto.cs
public class PotGroupsHistoryDto
=======
public class PotGroupsHistoryDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/PotGroupsHistory.cs
{
    public Guid Id { get; set; }
    public Guid PotGroupId { get; set; }
    public Guid PotId { get; set; }
    public DateTime Date { get; set; }
}