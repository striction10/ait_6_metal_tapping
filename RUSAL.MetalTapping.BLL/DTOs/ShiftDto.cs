namespace RUSAL.MetalTapping.BLL.DTOs
{
    public class ShiftDto
    {
        public int Id { get; set; }
        public int WorkGroupId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
