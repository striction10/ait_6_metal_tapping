namespace RUSAL.MetalTapping.BLL.Application.DTOs;

public class BuildingMetalInfoViewModel
{
    public Guid BuildingId { get; set; }
    public Guid MetalMarkId { get; set; }
    public double TotalMetalWeight { get; set; }
    public List<PotGroupViewModel> Groups { get; set; }
}