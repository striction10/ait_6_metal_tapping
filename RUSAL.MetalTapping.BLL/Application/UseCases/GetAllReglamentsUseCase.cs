using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.UseCases
{
    public class GetAllReglamentsUseCase
    {
        private readonly IReglamentRepository _repository;

        public GetAllReglamentsUseCase(IReglamentRepository repository)
        {
            _repository = repository;
        }

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
}
