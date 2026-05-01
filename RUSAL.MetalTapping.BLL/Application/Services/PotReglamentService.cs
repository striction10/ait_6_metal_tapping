using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class PotReglamentService(
    IPotReglamentRepository potReglamentRepository,
    IMapper mapper)
{
    private readonly IPotReglamentRepository _potReglamentRepository = potReglamentRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение списка записей о регламенте электролизёров внутри корпуса
    /// </summary>
    /// <param name="reglamentId"> Идентификатор регламента </param>
    /// <param name="buildingId"> Идентификатор электролизёра </param>
    /// <returns> DTO записи о регламенте </returns>
    public async Task<IEnumerable<PotReglamentDto?>> GetByReglamentAndBuildingIdAsync(Guid reglamentId, Guid buildingId)
    {
        var entities = EnsureFound(await _potReglamentRepository.GetByReglamentAndBuildingWithDeviationsAsync(reglamentId, buildingId),
            $"PotReglament for building {buildingId} and reglament {reglamentId} was not found");

        return _mapper.Map<IEnumerable<PotReglamentDto>>(entities);
    }
}