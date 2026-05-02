using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IDeviationValuesRepository : IGenericRepository<DeviationValues>
{
    Task<IEnumerable<DeviationValues>> GetDeviationValuesWithDeviationId(Guid id);
}
