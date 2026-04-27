using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Interfaces;
using MetalMarkDto = RUSAL.MetalTapping.BLL.Domain.DTOs.MetalMarkDto;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class GetAllMetalMarksUseCase(IGenericRepository<MetalMarkDto> repository)
{
    private readonly IGenericRepository<MetalMarkDto> _repository = repository;

    /// <summary>
    /// Получение списка марок металла
    /// </summary>
    /// <returns> Список марок металла </returns>
    public async Task<IEnumerable<ViewModels.MetalMarkViewModel>> ExecuteAsync()
    {
        var metalMarks = await _repository.GetAllAsync();

        return metalMarks.Select(m => new ViewModels.MetalMarkViewModel 
        {
            Id = m.Id,
            Name = m.Name
        });
    }
}