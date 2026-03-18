namespace RUSAL.MetalTapping.DAL.Models
{
    public class PotModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid StateId { get; set; }
        public Guid BuildingId { get; set; }
        public BuildingModel Building { get; set; }
        public PotStateModel State { get; set; }
        public ICollection<MetalMarkAnalysisModel> metalMarkAnalyses { get; set; } = new List<MetalMarkAnalysisModel>();
        public ICollection<PotReglamentModel> Reglaments = new List<PotReglamentModel>();
        public ICollection<CalculatedTaskModel> calculatedTasks = new List<CalculatedTaskModel>();
        public ICollection<ExternalDataModel> ExternalDatas = new List<ExternalDataModel>();
        public ICollection<TapTaskPotModel> TapTaskPots = new List<TapTaskPotModel>();
    }
}