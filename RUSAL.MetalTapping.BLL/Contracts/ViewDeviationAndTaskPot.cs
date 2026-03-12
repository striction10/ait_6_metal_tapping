namespace RUSAL.MetalTapping.BLL.Contracts
{
    public record ViewDeviationAndTaskPot(
        string potName,
        double targetMetalLevel,
        double? actualMetalLevel,
        double? deviationValue,
        double? calculatedTask,
        double? roundCalculatedTask,
        string metalMarkName);
}
