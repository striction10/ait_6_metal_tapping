using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class GetAllMetalMarksUseCase(IGenericRepository<MetalMark> repository)
{
    private readonly IGenericRepository<MetalMark> _repository = repository;

    /// <summary>
    /// Получение списка марок металла
    /// </summary>
    /// <returns> Список марок металла </returns>
    public async Task<IEnumerable<MetalMarkDto>> ExecuteAsync()
    {
        var metalMarks = await _repository.GetAllAsync();

        return metalMarks.Select(m => new MetalMarkDto 
        {
            Id = m.Id,
            Name = m.Name
        });
    }
}