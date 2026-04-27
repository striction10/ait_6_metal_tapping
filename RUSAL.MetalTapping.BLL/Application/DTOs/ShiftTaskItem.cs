namespace RUSAL.MetalTapping.BLL.Application.DTOs;

public class ShiftTaskItemViewModel
{
    public DateTime Time { get; init; }
    public double Weight { get; init; }
    public string PotName { get; init; }
    public string ScoopName { get; init; }
    public string MetalGrade { get; init; }
    public List<ChemicalElemDto> elements { get; set; } = new List<ChemicalElemDto>();
}