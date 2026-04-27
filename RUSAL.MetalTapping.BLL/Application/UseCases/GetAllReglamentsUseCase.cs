using RUSAL.MetalTapping.BLL.Application.ViewModels;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class GetAllReglamentsUseCase(IReglamentRepository repository)
{
    private readonly IReglamentRepository _repository = repository;

    /// <summary>
    /// Получение списка регламентов
    /// </summary>
    /// <returns> Список регламентов </returns>
    public async Task<IEnumerable<ReglamentViewModel>> ExecuteAsync()
    {
        var reglaments = await _repository.GetAllAsync();

        return reglaments.Select(r => new ReglamentViewModel
        {
            Id = r.Id,
            Name = r.Name
        });
    }
}