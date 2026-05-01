using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class PotGroupsHistory : IEntity
{
    public Guid Id { get; set; }
    public Guid PotGroupId { get; set; }
    public Guid PotId { get; set; }
    public DateTime Date { get; set; }
    public PotGroup PotGroup { get; set; } = null!;
    public Pot Pot { get; set; } = null!;
}
