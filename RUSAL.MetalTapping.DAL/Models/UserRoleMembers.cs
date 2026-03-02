namespace RUSAL.MetalTapping.DAL.Models
{
    public class UserRoleMembers
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public Role Role { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}