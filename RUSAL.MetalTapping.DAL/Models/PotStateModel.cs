namespace RUSAL.MetalTapping.DAL.Models;

public class PotState
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<PotModel> Pots = new List<PotModel>();
}