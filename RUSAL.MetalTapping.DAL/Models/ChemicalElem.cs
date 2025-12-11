namespace RUSAL.MetalTapping.DAL.Models
{
    public class ChemicalElem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<MetalMarkAnalysisValue> Values = new  List<MetalMarkAnalysisValue>();
    }
}