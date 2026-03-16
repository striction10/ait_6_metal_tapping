namespace RUSAL.MetalTapping.BLL.Application.Contracts
{
    public record ProcessCalculatedTaskResponse(
        double calculatedTask, 
        double? roundCalculatedTask = null);
}