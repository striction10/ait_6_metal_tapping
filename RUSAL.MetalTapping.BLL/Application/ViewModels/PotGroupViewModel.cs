namespace RUSAL.MetalTapping.BLL.Application.ViewModels;

public class PotGroupViewModel
{
    public Guid Id { get; set; }
    public double GroupMetalWeight { get; set; }
    public ScoopViewModel Scoop { get; set; }
    public List<PotViewModel> Pots { get; set; }
}
