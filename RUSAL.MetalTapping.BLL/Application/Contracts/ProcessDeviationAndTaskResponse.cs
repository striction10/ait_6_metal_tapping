namespace RUSAL.MetalTapping.BLL.Application.Contracts
{
    public record ProcessDeviationAndTaskResponse(double deviation, double? calculatedTask = null, double? roundCalculatedTask = null);
}