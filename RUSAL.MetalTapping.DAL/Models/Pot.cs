namespace RUSAL.MetalTapping.DAL.Models
{
    public class Pot
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; } = null!;
        public PotState State { get; set; } = null!;
        public ICollection<PotReglament> Reglaments = new List<PotReglament>();
        public ICollection<ExternalData> ExternalDatas = new List<ExternalData>();
        public ICollection<TapTaskPot> TapTaskPots = new List<TapTaskPot>();
    }
}