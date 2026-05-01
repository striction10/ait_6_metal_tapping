namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/TapTaskPotDto.cs
public class TapTaskPotDto
=======
<<<<<<< Updated upstream:RUSAL.MetalTapping.BLL/Domain/Entities/TapTaskPot.cs
public class TapTaskPot : IDomain
=======
namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class TapTaskPotDto : IDomain
>>>>>>> Stashed changes:RUSAL.MetalTapping.BLL/Domain/DTOs/TapTaskPotDto.cs
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/TapTaskPot.cs
{
    public Guid Id { get; set; }
    public Guid TapTaskId { get; set; }
    public Guid PotId { get; set; }
    public Guid MetalMarkAnalysisId { get; set; }
    public double PotMetalWeigth { get; set; }
}