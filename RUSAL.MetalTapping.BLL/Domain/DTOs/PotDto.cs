namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/PotDto.cs
public class PotDto
=======
public class PotDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/Pot.cs
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StateId { get; set; }
    public Guid BuildingId { get; set; }
}