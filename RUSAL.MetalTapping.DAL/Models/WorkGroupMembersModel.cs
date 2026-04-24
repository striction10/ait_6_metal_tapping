namespace RUSAL.MetalTapping.DAL.Models;

public class WorkGroupMembersModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkGroupId { get; set; }
    public WorkGroupModel WorkGroup { get; set; } = null!;
    public UserModel User { get; set; } = null!;
}