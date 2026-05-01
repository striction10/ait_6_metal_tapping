using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class PotParametersGroup : IEntity
{
    public Guid Id { get; set; }
    public ICollection<PotParameter> Parameters { get; set; } = new List<PotParameter>();
}