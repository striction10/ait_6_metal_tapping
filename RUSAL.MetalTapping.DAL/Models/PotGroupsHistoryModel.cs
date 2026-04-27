namespace RUSAL.MetalTapping.DAL.Models;

public class PotGroupsHistory
{
    public Guid Id { get; set; }
    public Guid PotGroupId { get; set; }
    public Guid PotId { get; set; }
    public DateTime Date { get; set; }
<<<<<<< Updated upstream:RUSAL.MetalTapping.DAL/Models/PotGroupsHistoryModel.cs
    public PotGroupModel PotGroup { get; set; }
    public PotModel Pot { get; set; }
=======
    public PotGroup PotGroup { get; set; }
    public Pot Pot { get; set; }
>>>>>>> Stashed changes:RUSAL.MetalTapping.DAL/Entities/PotGroupsHistory.cs
}
