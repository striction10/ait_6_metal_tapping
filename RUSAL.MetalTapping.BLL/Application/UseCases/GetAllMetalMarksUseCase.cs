using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class GetAllMetalMarksUseCase(MetalMarkService metalMarkService)
{
    private readonly MetalMarkService _metalMarkService = metalMarkService;

    /// <summary>
    /// Получение списка марок металла
    /// </summary>
    /// <returns> Список марок металла </returns>
    public async Task<IEnumerable<MetalMarkViewModel>> ExecuteAsync()
    {
        var metalMarks = await _metalMarkService.GetAllAsync();

        return metalMarks.Select(m => new MetalMarkViewModel 
        {
            Id = m.Id,
            Name = m.Name
        });
    }
}