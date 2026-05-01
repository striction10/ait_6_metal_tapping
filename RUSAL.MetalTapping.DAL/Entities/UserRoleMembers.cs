using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class UserRoleMembers : IEntity
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }
    public Role Role { get; set; } = null!;
    public User User { get; set; } = null!;
}