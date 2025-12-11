namespace RUSAL.MetalTapping.DAL.Models
{
    public class WorkGroupMembers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkGroupId { get; set; }
        public WorkGroup WorkGroup { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}