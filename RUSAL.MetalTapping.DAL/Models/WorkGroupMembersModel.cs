namespace RUSAL.MetalTapping.DAL.Models;

public class WorkGroupMembers
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkGroupId { get; set; }
<<<<<<< Updated upstream:RUSAL.MetalTapping.DAL/Models/WorkGroupMembersModel.cs
    public WorkGroupModel WorkGroup { get; set; } = null!;
    public UserModel User { get; set; } = null!;
=======
    public WorkGroup WorkGroup { get; set; } = null!;
    public User User { get; set; } = null!;
>>>>>>> Stashed changes:RUSAL.MetalTapping.DAL/Entities/WorkGroupMembers.cs
}