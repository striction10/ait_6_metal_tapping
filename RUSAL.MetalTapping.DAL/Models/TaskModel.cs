namespace RUSAL.MetalTapping.DAL.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public Guid TapTaskId { get; set; }
        public Guid ShiftId { get; set; }
        public DateTime LeadTime { get; set; }
        public ShiftModel Shift { get; set; } = null!;
        public TapTaskModel TapTask { get; set; } = null!;
    }
}