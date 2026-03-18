namespace RUSAL.MetalTapping.BLL.Domain.Entities
{
    public class ShiftTask
    {
        public Guid Id { get; set; }
        public Guid TapTaskId { get; set; }
        public Guid ShiftId { get; set; }
    }
}