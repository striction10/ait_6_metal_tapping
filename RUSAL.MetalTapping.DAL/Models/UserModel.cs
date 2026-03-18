namespace RUSAL.MetalTapping.DAL.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<UserRoleMembersModel>  UserRoleMembers { get; set; } = new List<UserRoleMembersModel>();
        public ICollection<WorkGroupMembersModel> WorkGroupMembers { get; set; } = new List<WorkGroupMembersModel>();
    }
}