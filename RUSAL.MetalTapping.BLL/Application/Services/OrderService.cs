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
    IOrderRepository orderRepository,
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

    /// <summary>
    /// Получение следующего в очереди заказа.
    /// </summary>
    /// <param name="ct"> Cancellation Token.</param>
    /// <returns> DTO заказа.</returns>
    public async Task<OrderDto?> GetNextPendingOrderAsync(CancellationToken ct)
    {
        var entity = await orderRepository.GetNextPendingOrderAsync(ct);

        return mapper.Map<OrderDto>(entity);
    }

    /// <summary>
    /// Обновление записи о заказе.
    /// </summary>
    /// <param name="dto"> DTO заказа. </param>
    public async Task UpdateAsync(OrderDto dto)
    {
        var entity = await orderRepository.GetByIdAsync(dto.Id);

        mapper.Map(dto, entity);

        await orderRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Сохранение изменений.
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await orderRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Создание отложенного в очередь заказа.
    /// </summary>
    /// <param name="weight">Вес невылитого металла в заказе.</param>
    /// <param name="metalMarkId">Идентификатор марки металла.</param>
    /// <returns>DTo заказа</returns>
    public async Task<OrderDto> AcceptOrderAsync(double weight, Guid metalMarkId)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            WeightOfMetal = weight,
            RemainingWeight = (decimal)weight,
            MetalMarkId = metalMarkId,
            DateOfOrder = DateTime.UtcNow,
            Status = 0,
        };

        await orderRepository.CreateAsync(order);

        return mapper.Map<OrderDto>(order);
    }
}
