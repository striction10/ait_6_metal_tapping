namespace RUSAL.MetalTapping.BLL.Application.DTOs
{
    public class ShiftTaskBlock
    {
        public List<ShiftTaskItem> Items { get; init; } = new();
        public double TotalWeight { get; init; }
    }
}