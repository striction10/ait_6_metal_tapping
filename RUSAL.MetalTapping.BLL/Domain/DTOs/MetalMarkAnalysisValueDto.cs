namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class MetalMarkAnalysisValueDto
{
    public Guid Id { get; set; }
    public Guid ChemicalElemId { get; set; }
    public Guid MetalMarkAnalysisId { get; set; }
    public double Value { get; set; }
}
