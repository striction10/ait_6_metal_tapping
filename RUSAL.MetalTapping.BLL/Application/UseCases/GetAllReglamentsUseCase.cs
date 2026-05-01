using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Application.ViewModels;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class GetAllReglamentsUseCase(ReglamentService reglamentService)
{
    private readonly ReglamentService _reglamentService = reglamentService;

    /// <summary>
    /// Получение списка регламентов
    /// </summary>
    /// <returns> Список регламентов </returns>
    public async Task<IEnumerable<ReglamentViewModel>> ExecuteAsync()
    {
        var reglaments = await _reglamentService.GetAllAsync();

        return reglaments.Select(r => new ReglamentViewModel
        {
            Id = r.Id,
            Name = r.Name
        });
    }
}