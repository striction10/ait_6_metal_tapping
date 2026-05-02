using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с ковшами.
/// </summary>
/// <param name="scoopRepository">Репозиторий ковшей.</param>
/// <param name="mapper">Маппер объектов.</param>
public class ScoopService(
    IGenericRepository<Scoop> scoopRepository,
    IMapper mapper)
{
    private readonly IGenericRepository<Scoop> scoopRepository = scoopRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Получение ковша по идентификатору.
    /// </summary>
    /// <param name="scoopId"> Идентификатор ковша. </param>
    /// <returns> DTO ковша. </returns>
    public async Task<ScoopDto?> GetByIdAsync(Guid scoopId)
    {
        var entity = EnsureFound(
            await scoopRepository.GetByIdAsync(scoopId),
            $"Scoop with id {scoopId} was not found");

        return mapper.Map<ScoopDto?>(entity);
    }
}
