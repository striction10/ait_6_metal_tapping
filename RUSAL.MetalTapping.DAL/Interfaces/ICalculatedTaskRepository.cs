using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface ICalculatedTaskRepository : IGenericRepository<CalculatedTask>
    {
        Task<CalculatedTask?> GetCalculatedTaskWithPotIdAsync(Guid id);
    }
}
