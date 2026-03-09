namespace RUSAL.MetalTapping.BLL.Contracts
{
    public record ProcessDeviationAndTaskResponse(double deviation, double? calculatedTask = null, double? roundCalculatedTask = null);
}