using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class PotGroup : IEntity
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public Guid ScoopId { get; set; }
    public Building Building { get; set; } = null!;
    public Scoop Scoop { get; set; } = null!;
    public ICollection<PotGroupsHistory> History { get; set; } = new List<PotGroupsHistory>();
}
