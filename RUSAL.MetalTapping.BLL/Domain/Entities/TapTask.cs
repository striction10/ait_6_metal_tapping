using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class TapTask : IDomain
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ScoopId { get; set; }
}