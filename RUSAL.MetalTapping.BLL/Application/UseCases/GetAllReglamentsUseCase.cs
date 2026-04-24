using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class GetAllReglamentsUseCase(IReglamentRepository repository)
{
    private readonly IReglamentRepository _repository = repository;

    /// <summary>
    /// Получение списка регламентов
    /// </summary>
    /// <returns> Список регламентов </returns>
    public async Task<IEnumerable<ReglamentDto>> ExecuteAsync()
    {
        var reglaments = await _repository.GetAllAsync();

        return reglaments.Select(r => new ReglamentDto
        {
            Id = r.Id,
            Name = r.Name
        });
    }
}