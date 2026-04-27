namespace RUSAL.MetalTapping.BLL.Application.DTOs;

public class ShiftTaskBlockViewModel
{
    public List<ShiftTaskItemViewModel> Items { get; init; } = new();
    public double TotalWeight { get; set; }
}