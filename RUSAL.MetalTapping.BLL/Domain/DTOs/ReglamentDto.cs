namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/ReglamentDto.cs
public class ReglamentDto
=======
public class ReglamentDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/Reglament.cs
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateStart { get; set; }
    public DateTime DateStop { get; set; }
}