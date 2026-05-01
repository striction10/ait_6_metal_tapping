using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.Services;

public class OrderService(
    IGenericRepository<Order> orderRepository,
    IMapper mapper)
{
    private readonly IGenericRepository<Order> _orderRepository = orderRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Создание записи о заказе в базе данных
    /// </summary>
    /// <param name="dto"> DTO заказа </param>
    public async Task CreateAsync(OrderDto dto)
    {
        var entity = _mapper.Map<Order>(dto);

        await _orderRepository.CreateAsync(entity);
    }
}