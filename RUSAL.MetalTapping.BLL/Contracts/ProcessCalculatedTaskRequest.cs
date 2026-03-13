namespace RUSAL.MetalTapping.BLL.Contracts
{
    public record ProcessCalculatedTaskRequest(
        Guid potId,
        double calculatedTask,
        double? roundedCalculatedTask = null);
}