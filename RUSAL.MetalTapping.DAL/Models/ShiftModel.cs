namespace RUSAL.MetalTapping.DAL.Models;

public class ShiftModel
{
    public Guid Id { get; set; }
    public Guid WorkGroupId { get; set; }
    public Guid BuildingId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public BuildingModel Building { get; set; } = null!;
    public WorkGroupModel WorkGroup { get; set; } = null!;
    public ICollection<TaskModel> Tasks { get; set; } = null!;
}