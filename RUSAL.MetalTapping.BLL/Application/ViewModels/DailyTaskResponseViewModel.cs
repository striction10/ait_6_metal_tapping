namespace RUSAL.MetalTapping.BLL.Application.ViewModels;

public class DailyTaskResponseViewModel
{
    public DateTime Date { get; init; }
    public ShiftTaskBlockViewModel NightShift { get; init; }
    public ShiftTaskBlockViewModel DayShift { get; init; }
    public DailySummaryViewModel Summary { get; init; }
}
