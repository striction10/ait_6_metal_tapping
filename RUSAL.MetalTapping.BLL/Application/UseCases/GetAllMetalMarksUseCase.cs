using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.UseCases
{
    public class GetAllMetalMarksUseCase
    {
        private readonly IGenericRepository<MetalMark> _repository;

        public GetAllMetalMarksUseCase(IGenericRepository<MetalMark> repository)
        {
            _repository = repository;
        }

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
}