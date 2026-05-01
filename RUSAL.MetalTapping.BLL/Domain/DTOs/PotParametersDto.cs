using RUSAL.MetalTapping.BLL.Domain.Enums;
namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/PotParametersDto.cs
public class PotParametersDto
=======
public class PotParametersDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/PotParameters.cs
{
    public Guid Id { get; set; }
    public PotParametersType Type { get; set; }
    public double Value { get; set; }
    public Guid PotParametersGroupId { get; set; }
}