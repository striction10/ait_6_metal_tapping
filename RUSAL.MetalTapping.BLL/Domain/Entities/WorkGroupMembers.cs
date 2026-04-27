using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class WorkGroupMembersDto : IDomain
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkGroupId { get; set; }
}