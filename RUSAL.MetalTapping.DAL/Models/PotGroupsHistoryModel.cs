namespace RUSAL.MetalTapping.DAL.Models
{
    public class PotGroupsHistoryModel
    {
        public Guid Id { get; set; }
        public Guid PotGroupId { get; set; }
        public Guid PotId { get; set; }
        public DateTime Date { get; set; }
        public PotGroupModel PotGroup { get; set; }
        public PotModel Pot { get; set; }
    }
}
