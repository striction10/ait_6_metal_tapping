namespace RUSAL.MetalTapping.DAL.Models
{
    public class WorkGroupMembers
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid WorkGroupId { get; set; }
        public WorkGroup WorkGroup { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}