using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class CalculatedTask : IEntity
{
    public Guid Id { get; set; }
    public Guid PotId { get; set; }
    public double? CalculatedTaskForPot { get; set; }
    public double? RoundCalculatedTaskForPot { get; set; }
    public DateTime CreatedAt { get; set; }
    public Pot Pot { get; set; } = null!;
}