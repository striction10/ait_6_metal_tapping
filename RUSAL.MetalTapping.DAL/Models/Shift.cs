namespace RUSAL.MetalTapping.DAL.Models
{
    public class Shift
    {
        public Guid Id { get; set; }
        public Guid WorkGroupId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public WorkGroup WorkGroup { get; set; } = null!;
    }
}