using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface ICalculatedTaskRepository : IGenericRepository<CalculatedTask>
{
    Task<CalculatedTask?> GetCalculatedTaskWithPotIdAsync(Guid id);
    Task<IEnumerable<CalculatedTask?>> GetByPotIdsAsync(IEnumerable<Guid> potIds);
}