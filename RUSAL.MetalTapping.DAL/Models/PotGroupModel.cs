namespace RUSAL.MetalTapping.DAL.Models;

public class PotGroup
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public Guid ScoopId { get; set; }
<<<<<<< Updated upstream:RUSAL.MetalTapping.DAL/Models/PotGroupModel.cs
    public BuildingModel Building { get; set; }
    public ScoopModel Scoop { get; set; }
    public ICollection<PotGroupsHistoryModel> History { get; set; } = new List<PotGroupsHistoryModel>();
=======
    public Building Building { get; set; }
    public Scoop Scoop { get; set; }
    public ICollection<PotGroupsHistory> History { get; set; } = new List<PotGroupsHistory>();
>>>>>>> Stashed changes:RUSAL.MetalTapping.DAL/Entities/PotGroup.cs
}