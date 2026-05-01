using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class WorkGroup : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    public ICollection<WorkGroupMembers> WorkGroupMembers { get; set; } = new List<WorkGroupMembers>();
}