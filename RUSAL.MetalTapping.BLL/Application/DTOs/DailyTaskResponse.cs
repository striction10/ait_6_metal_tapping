namespace RUSAL.MetalTapping.BLL.Application.DTOs
{
    public class DailyTaskResponse
    {
        public DateTime Date { get; init; }
        public ShiftTaskBlock NightShift { get; init; }
        public ShiftTaskBlock DayShift { get; init; }
        public DailySummary Summary { get; init; }
    }
}