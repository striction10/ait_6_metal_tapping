using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class MetalMarkAnalysisValue : IDomain
{
    public Guid Id { get; set; }
    public Guid ChemicalElemId { get; set; }
    public Guid MetalMarkAnalysisId { get; set; }
    public double Value { get; set; }
}
