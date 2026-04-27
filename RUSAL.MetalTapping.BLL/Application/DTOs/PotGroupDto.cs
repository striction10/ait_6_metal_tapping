namespace RUSAL.MetalTapping.BLL.Application.DTOs;

public class PotGroupViewModel
{
    public Guid Id { get; set; }
    public double GroupMetalWeight { get; set; }
<<<<<<< Updated upstream:RUSAL.MetalTapping.BLL/Application/DTOs/PotGroupDto.cs
    public ScoopDto Scoop { get; set; }
    public List<PotDto> Pots { get; set; }
=======
    public ScoopViewModel Scoop { get; set; }
    public List<PotViewModel> Pots { get; set; }
>>>>>>> Stashed changes:RUSAL.MetalTapping.BLL/Application/ViewModels/PotGroupViewModel.cs
}