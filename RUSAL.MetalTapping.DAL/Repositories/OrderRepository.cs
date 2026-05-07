using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class OrderRepository(AppDbContext context)
    : GenericRepository<Order>(context), IOrderRepository
{
    public async Task<Order?> GetNextPendingOrderAsync(CancellationToken cancellationToken = default)
    {
        return await context.Orders
            .Where(o => o.RemainingWeight > 0 && o.Status == 0)
            .OrderBy(o => o.DateOfOrder)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
