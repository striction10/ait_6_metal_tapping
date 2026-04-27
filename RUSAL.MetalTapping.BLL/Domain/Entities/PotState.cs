using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class PotStateDto : IDomain
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}