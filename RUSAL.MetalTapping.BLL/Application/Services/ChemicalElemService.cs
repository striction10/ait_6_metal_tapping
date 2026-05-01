using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class ChemicalElemService(
    IGenericRepository<ChemicalElem> chemicalElemRepository,
    IMapper mapper)
{
    private readonly IGenericRepository<ChemicalElem> _chemicalElemRepository = chemicalElemRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение хим элемента по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор хим элемента </param>
    /// <returns> DTO хим элемента </returns>
    public async Task<ChemicalElemDto?> GetByIdAsync(Guid id)
    {
        var entity = EnsureFound(await _chemicalElemRepository.GetByIdAsync(id),
            $"Chemical elem with id {id} was not found");

        return _mapper.Map<ChemicalElemDto?>(entity);
    }
}