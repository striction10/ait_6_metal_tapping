namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class PotGroupsHistoryDto
{
    public Guid Id { get; set; }
    public Guid PotGroupId { get; set; }
    public Guid PotId { get; set; }
    public DateTime Date { get; set; }
}
