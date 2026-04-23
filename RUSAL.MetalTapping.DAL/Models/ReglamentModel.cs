namespace RUSAL.MetalTapping.DAL.Models;

public class ReglamentModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateStart { get; set; }
    public DateTime DateStop { get; set; }
    public ICollection<PotReglamentModel> Reglaments = new List<PotReglamentModel>();
}