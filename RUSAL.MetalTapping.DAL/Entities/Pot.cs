using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class Pot : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StateId { get; set; }
    public Guid BuildingId { get; set; }
    public Building Building { get; set; } = null!;
    public PotState State { get; set; } = null!;
    public ICollection<MetalMarkAnalysis> metalMarkAnalyses { get; set; } = new List<MetalMarkAnalysis>();
    public ICollection<PotReglament> Reglaments { get; set; } = new List<PotReglament>();
    public ICollection<CalculatedTask> calculatedTasks { get; set; } = new List<CalculatedTask>();
    public ICollection<ExternalData> ExternalDatas { get; set; } = new List<ExternalData>();
    public ICollection<TapTaskPot> TapTaskPots { get; set; } = new List<TapTaskPot>();
    public ICollection<PotGroupsHistory> GroupsHistory { get; set; } = new List<PotGroupsHistory>();
}
