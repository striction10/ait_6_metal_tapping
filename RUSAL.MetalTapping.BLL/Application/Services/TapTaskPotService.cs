using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class TapTaskPotService(
    ITapTaskPotRepository tapTaskPotRepository,
    IMapper mapper)
{
    private readonly ITapTaskPotRepository _tapTaskPotRepository = tapTaskPotRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение задания на выливку для электролизера по заданию на выливку
    /// </summary>
    /// <param name="tapTaskId"> Идентификатор задания на выливку </param>
    /// <returns> DTO задания на выливку для электролизёра </returns>
    public async Task<IEnumerable<TapTaskPotDto?>> GetByTapTaskIdAsync(Guid tapTaskId)
    {
        var entity = EnsureFound(await _tapTaskPotRepository.GetByTapTaskId(tapTaskId),
            $"Task pot for tap task {tapTaskId} was not found");

        return _mapper.Map<IEnumerable<TapTaskPotDto?>>(entity);
    }

    /// <summary>
    /// Создание записи о задании на выливку для электролизёра в базе данных
    /// </summary>
    /// <param name="dto"> DTO записи о задании на выливку для электролизёра </param>
    public async Task CreateAsync(TapTaskPotDto dto)
    {
        var entity = _mapper.Map<TapTaskPot>(dto);

        await _tapTaskPotRepository.CreateAsync(entity);
    }
}