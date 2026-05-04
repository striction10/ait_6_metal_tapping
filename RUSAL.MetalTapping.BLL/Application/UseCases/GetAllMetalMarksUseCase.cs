using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.ViewModels;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор получения списка всех марок металла.
/// </summary>
/// <param name="metalMarkService">Сервис для работы с марками металла.</param>
public class GetAllMetalMarksUseCase(MetalMarkService metalMarkService)
{
    /// <summary>
    /// Получение списка марок металла.
    /// </summary>
    /// <returns> Список марок металла. </returns>
    public async Task<IEnumerable<MetalMarkViewModel>> ExecuteAsync()
    {
        var metalMarks = await metalMarkService.GetAllAsync();

        return metalMarks.Select(m => new MetalMarkViewModel
        {
            Id = m.Id,
            Name = m.Name,
        });
    }
}
