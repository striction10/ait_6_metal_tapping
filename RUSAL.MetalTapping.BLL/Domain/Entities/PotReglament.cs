using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class PotReglamentDto : IDomain
{
    public Guid Id { get; set; }
    public Guid ReglamentId { get; set; }
    public Guid PotId { get; set; }
    public List<Deviation> Deviations { get; set; } = new();
}