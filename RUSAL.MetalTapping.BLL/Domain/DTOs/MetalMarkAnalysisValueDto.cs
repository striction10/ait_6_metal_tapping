namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/MetalMarkAnalysisValueDto.cs
public class MetalMarkAnalysisValueDto
=======
public class MetalMarkAnalysisValueDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/MetalMarkAnalysisValue.cs
{
    public Guid Id { get; set; }
    public Guid ChemicalElemId { get; set; }
    public Guid MetalMarkAnalysisId { get; set; }
    public double Value { get; set; }
}