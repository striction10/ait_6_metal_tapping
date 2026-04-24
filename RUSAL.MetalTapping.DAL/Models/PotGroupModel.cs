namespace RUSAL.MetalTapping.DAL.Models;

public class PotGroupModel
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public Guid ScoopId { get; set; }
    public BuildingModel Building { get; set; }
    public ScoopModel Scoop { get; set; }
    public ICollection<PotGroupsHistoryModel> History { get; set; } = new List<PotGroupsHistoryModel>();
}