namespace RUSAL.MetalTapping.BLL.Application.ViewModels;

public class BuildingViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<PotGroupViewModel> Groups { get; set; }
}
