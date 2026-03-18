namespace RUSAL.MetalTapping.BLL.Application.Contracts
{
    public record ProcessRoundTaskRequest(
        Guid potId,
        double roundTask);
}
