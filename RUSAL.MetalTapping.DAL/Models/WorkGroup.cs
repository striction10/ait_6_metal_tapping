namespace RUSAL.MetalTapping.DAL.Models
{
    public class WorkGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
        public ICollection<WorkGroupMembers> WorkGroupMembers { get; set; } = new List<WorkGroupMembers>();
    }
}