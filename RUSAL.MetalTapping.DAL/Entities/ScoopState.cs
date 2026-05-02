using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class ScoopState : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Scoop> Scoops { get; set; } = new List<Scoop>();
}
