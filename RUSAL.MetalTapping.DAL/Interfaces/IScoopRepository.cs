using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Interfaces;

public interface IScoopRepository : IGenericRepository<Scoop>
{
    Task<Scoop?> GetFirstAvailableScoopAsync(CancellationToken cancellationToken = default);
}
