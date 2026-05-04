using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с заказами.
/// </summary>
/// <param name="orderRepository">Репозиторий заказов.</param>
/// <param name="mapper">Маппер объектов.</param>
public class OrderService(
    IGenericRepository<Order> orderRepository,
    IMapper mapper)
{
    /// <summary>
    /// Создание записи о заказе в базе данных.
    /// </summary>
    /// <param name="dto"> DTO заказа. </param>
    public async Task CreateAsync(OrderDto dto)
    {
        var entity = mapper.Map<Order>(dto);

        await orderRepository.CreateAsync(entity);
    }
}
