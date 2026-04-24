namespace RUSAL.MetalTapping.BLL.Application.Contracts;

public record ProcessDeviationAndTaskRequest(Guid potId, double actualMetalLevel);