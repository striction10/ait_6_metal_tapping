namespace RUSAL.MetalTapping.BLL.Application.DTOs;

public class ExecutionSegmentViewModel
{
    public Guid BuildingId { get; set; }
    public Guid GroupId { get; set; }
    public Guid ScoopId { get; set; }
    public List<Guid> PotIds { get; set; }
    public double MetalWeight { get; set; }
}