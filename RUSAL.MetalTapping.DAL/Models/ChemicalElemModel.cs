namespace RUSAL.MetalTapping.DAL.Models;

public class ChemicalElemModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<MetalMarkAnalysisValueModel> Values = new  List<MetalMarkAnalysisValueModel>();
}