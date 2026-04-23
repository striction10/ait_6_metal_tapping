using RUSAL.MetalTapping.BLL.Domain.Entities;
namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IDeviationValuesRepository : IGenericRepository<DeviationValues>
{
    Task<IEnumerable<DeviationValues>> GetDeviationValuesWithDeviationId(Guid id);
}