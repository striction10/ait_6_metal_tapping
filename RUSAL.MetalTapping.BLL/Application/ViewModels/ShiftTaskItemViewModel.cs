namespace RUSAL.MetalTapping.BLL.Application.ViewModels;

public class ShiftTaskItemViewModel
{
    public DateTime Time { get; init; }
    public double Weight { get; init; }
    public string PotName { get; init; }
    public string ScoopName { get; init; }
    public string MetalGrade { get; init; }
    public List<ChemicalElemViewModel> elements { get; set; } = new List<ChemicalElemViewModel>();
}