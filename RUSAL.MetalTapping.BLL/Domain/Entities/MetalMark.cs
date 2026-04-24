using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class MetalMark : IDomain
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
