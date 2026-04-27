namespace RUSAL.MetalTapping.DAL.Models;

public class Building
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Pot> Pots { get; set; } = new List<Pot>();
    public ICollection<TapTask> TapTasks { get; set; } = new List<TapTask>();
    public ICollection<PotGroup> PotGroupModels { get; set; } = new List<PotGroup>();
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}