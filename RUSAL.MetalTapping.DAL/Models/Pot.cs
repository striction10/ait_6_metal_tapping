namespace RUSAL.MetalTapping.DAL.Models
{
    public class Pot
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid StateId { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
        public PotState State { get; set; }
        public ICollection<PotReglament> Reglaments = new List<PotReglament>();
        public ICollection<CalculatedTask> calculatedTasks = new List<CalculatedTask>();
        public ICollection<ExternalData> ExternalDatas = new List<ExternalData>();
        public ICollection<TapTaskPot> TapTaskPots = new List<TapTaskPot>();
    }
}