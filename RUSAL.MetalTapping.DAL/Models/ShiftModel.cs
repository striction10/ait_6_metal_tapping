namespace RUSAL.MetalTapping.DAL.Models;

public class Shift
{
    public Guid Id { get; set; }
    public Guid WorkGroupId { get; set; }
    public Guid BuildingId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
<<<<<<< Updated upstream:RUSAL.MetalTapping.DAL/Models/ShiftModel.cs
    public BuildingModel Building { get; set; } = null!;
    public WorkGroupModel WorkGroup { get; set; } = null!;
    public ICollection<TaskModel> Tasks { get; set; } = null!;
=======
    public Building Building { get; set; } = null!;
    public WorkGroup WorkGroup { get; set; } = null!;
    public ICollection<Task> Tasks { get; set; } = null!;
>>>>>>> Stashed changes:RUSAL.MetalTapping.DAL/Entities/Shift.cs
}