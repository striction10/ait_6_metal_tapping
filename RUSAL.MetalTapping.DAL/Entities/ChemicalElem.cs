using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class ChemicalElem : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<MetalMarkAnalysisValue> Values { get; set; } = new List<MetalMarkAnalysisValue>();
}