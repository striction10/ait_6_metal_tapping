namespace RUSAL.MetalTapping.BLL.DTOs
{
    public class DeviationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double TargetMetalLevel { get; set; }
        public int PotReglamentId { get; set; }
    }
}
