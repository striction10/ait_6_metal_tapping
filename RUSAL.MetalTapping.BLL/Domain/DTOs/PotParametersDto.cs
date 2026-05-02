using RUSAL.MetalTapping.BLL.Domain.Enums;

namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class PotParametersDto
{
    public Guid Id { get; set; }
    public PotParametersType Type { get; set; }
    public double Value { get; set; }
    public Guid PotParametersGroupId { get; set; }
}
