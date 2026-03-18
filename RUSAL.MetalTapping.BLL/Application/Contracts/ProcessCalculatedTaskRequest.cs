namespace RUSAL.MetalTapping.BLL.Application.Contracts
{
    public record ProcessCalculatedTaskRequest(
        Guid potId,
        double calculatedTask);
}