using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class Reglament : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateStart { get; set; }
    public DateTime DateStop { get; set; }
    public ICollection<PotReglament> Reglaments { get; set; } = new List<PotReglament>();
}
