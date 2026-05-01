namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

public class PotReglamentDto
{
    public Guid Id { get; set; }
    public Guid ReglamentId { get; set; }
    public Guid PotId { get; set; }
    public List<DeviationDto> Deviations { get; set; } = new();
}