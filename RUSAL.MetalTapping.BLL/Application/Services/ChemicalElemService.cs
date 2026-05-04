using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с химическими элементами.
/// </summary>
/// <param name="chemicalElemRepository">Репозиторий химических элементов.</param>
/// <param name="mapper">Маппер объектов.</param>
public class ChemicalElemService(
    IGenericRepository<ChemicalElem> chemicalElemRepository,
    IMapper mapper)
{
    /// <summary>
    /// Получение хим элемента по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор хим элемента. </param>
    /// <returns> DTO хим элемента. </returns>
    public async Task<ChemicalElemDto?> GetByIdAsync(Guid id)
    {
        var entity = EnsureFound(
            await chemicalElemRepository.GetByIdAsync(id),
            $"Chemical elem with id {id} was not found");

        return mapper.Map<ChemicalElemDto?>(entity);
    }
}
