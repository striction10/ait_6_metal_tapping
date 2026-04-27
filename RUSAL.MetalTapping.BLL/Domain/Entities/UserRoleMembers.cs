using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class UserRoleMembersDto : IDomain
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }
}