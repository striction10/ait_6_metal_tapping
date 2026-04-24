namespace RUSAL.MetalTapping.DAL.Models;

public class WorkGroupModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<ShiftModel> Shifts { get; set; } = new List<ShiftModel>();
    public ICollection<WorkGroupMembersModel> WorkGroupMembers { get; set; } = new List<WorkGroupMembersModel>();
}