using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ReglamentService(IReglamentRepository reglamentRepository, IMapper mapper)
{
    private readonly IReglamentRepository _reglamentRepository = reglamentRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение регламента по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор регламента </param>
    /// <returns> DTO регламента </returns>
    public async Task<ReglamentDto> GetByIdAsync(Guid id)
    {
        var entity = EnsureFound(await _reglamentRepository.GetByIdAsync(id),
            $"Reglament with id {id} was not found");

        return _mapper.Map<ReglamentDto>(entity);
    }

    /// <summary>
    /// Получение текущего действующего регламента 
    /// </summary>
    /// <returns> DTO действующего регламента </returns>
    public async Task<ReglamentDto> GetCurrentReglament()
    {
        var entity = EnsureFound(await _reglamentRepository.GetNewReglament(),
            "Current reglament was not found");

        return _mapper.Map<ReglamentDto>(entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<ReglamentDto>> GetAllAsync()
    {
        var entities = await _reglamentRepository.GetAllAsync();

        return _mapper.Map <IEnumerable<ReglamentDto>>(entities);
    }
}