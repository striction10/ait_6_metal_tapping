namespace RUSAL.MetalTapping.DAL.Models;

public class RoleModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<UserRoleMembersModel> UserRoleMembers { get; set; } = new List<UserRoleMembersModel>();
}