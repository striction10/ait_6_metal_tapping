namespace RUSAL.MetalTapping.DAL.Models
{
    public class UserRoleMembersModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public RoleModel Role { get; set; } = null!;
        public UserModel User { get; set; } = null!;
    }
}