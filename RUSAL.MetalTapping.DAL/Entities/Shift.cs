namespace RUSAL.MetalTapping.DAL.Entities
{
    public class Shift
    {
        public int Id { get; set; }
        public int WorkGroupId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
