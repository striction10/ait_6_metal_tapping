namespace RUSAL.MetalTapping.BLL.Contracts
{
    public record ProcessCalculatedTaskResponse(
        double calculatedTask, double?
        roundCalculatedTask = null);
}