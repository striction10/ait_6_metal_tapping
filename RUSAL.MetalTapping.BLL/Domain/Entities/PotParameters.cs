using RUSAL.MetalTapping.BLL.Domain.Enums;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Domain.Entities;

public class PotParametersDto : IDomain
{
    public Guid Id { get; set; }
    public PotParametersType Type { get; set; }
    public double Value { get; set; }
    public Guid PotParametersGroupId { get; set; }
}