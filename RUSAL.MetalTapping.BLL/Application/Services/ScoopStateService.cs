using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ScoopStateService(
    IGenericRepository<ScoopState> scoopStateRepository,
    IMapper mapper)
{
    private readonly IGenericRepository<ScoopState> _scoopStateRepository = scoopStateRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение состояние ковша по идентификатору состояния
    /// </summary>
    /// <param name="scoopStateId"> Идентификатор состояния </param>
    /// <returns> DTO состояния ковша </returns>
    public async Task<ScoopStateDto?> GetByIdAsync(Guid scoopStateId)
    {
        var entity = EnsureFound(await _scoopStateRepository.GetByIdAsync(scoopStateId),
            $"Scoop state with id {scoopStateId}");

        return _mapper.Map<ScoopStateDto?>(entity);
    }
}