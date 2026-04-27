namespace RUSAL.MetalTapping.BLL.Application.DTOs;

public class DailyTaskResponseViewModel
{
    public DateTime Date { get; init; }
<<<<<<< Updated upstream:RUSAL.MetalTapping.BLL/Application/DTOs/DailyTaskResponse.cs
    public ShiftTaskBlock NightShift { get; init; }
    public ShiftTaskBlock DayShift { get; init; }
    public DailySummary Summary { get; init; }
=======
    public ShiftTaskBlockViewModel NightShift { get; init; }
    public ShiftTaskBlockViewModel DayShift { get; init; }
    public DailySummaryViewModel SummaryViewModel { get; init; }
>>>>>>> Stashed changes:RUSAL.MetalTapping.BLL/Application/ViewModels/DailyTaskResponseViewModel.cs
}