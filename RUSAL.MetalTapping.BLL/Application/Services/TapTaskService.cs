using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с заданиями на выливку.
/// </summary>
/// <param name="tapTaskRepository">Репозиторий заданий на выливку.</param>
/// <param name="mapper">Маппер объектов.</param>
public class TapTaskService(
    IGenericRepository<TapTask> tapTaskRepository,
    IMapper mapper)
{
    /// <summary>
    /// Создание записи о задании на выливку в базе данных.
    /// </summary>
    /// <param name="dto"> DTO записи о задании на выливку. </param>
    public async Task CreateAsync(TapTaskDto dto)
    {
        var entity = mapper.Map<TapTask>(dto);

        await tapTaskRepository.CreateAsync(entity);
    }

    /// <summary>
    /// Получение записи о задании на выливку по идентификатору. 
    /// </summary>
    /// <param name="id"> Идентификатор записи о задании на выливку. </param>
    /// <returns> DTO записи о задании на выливку. </returns>
    public async Task<TapTaskDto?> GetByIdAsync(Guid id)
    {
        var entity = EnsureFound(
            await tapTaskRepository.GetByIdAsync(id),
            $"Tap task with id {id} was not found");

        return mapper.Map<TapTaskDto?>(entity);
    }
}
