using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.Contracts;

public record ReglamentTableResponse(List<PotDeviationDto> pots);