namespace RUSAL.MetalTapping.BLL.Application.ViewModels;

public class ShiftTaskBlockViewModel
{
    public List<ShiftTaskItemViewModel> Items { get; init; } = new();
    public double TotalWeight { get; set; }
}
