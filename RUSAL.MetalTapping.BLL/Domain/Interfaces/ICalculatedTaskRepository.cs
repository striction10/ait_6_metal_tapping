using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface ICalculatedTaskRepository : IGenericRepository<CalculatedTask>
{
    Task<CalculatedTask?> GetCalculatedTaskWithPotIdAsync(Guid id);
    Task<IEnumerable<CalculatedTask?>> GetByPotIdsAsync(IEnumerable<Guid> potIds);
}