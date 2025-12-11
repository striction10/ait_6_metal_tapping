namespace RUSAL.MetalTapping.DAL.Models
{
    public class UserRoleMembers
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}