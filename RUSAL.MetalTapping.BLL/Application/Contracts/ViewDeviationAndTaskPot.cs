namespace RUSAL.MetalTapping.BLL.Application.Contracts;

public record ViewDeviationAndTaskPot(
    Guid potId,
    string potName,
    double targetMetalLevel,
    double? actualMetalLevel,
    double? deviationValue,
    double amperage,
    double avgAmperage,
    double? calculatedTask,
    double? roundCalculatedTask,
    string metalMarkName);