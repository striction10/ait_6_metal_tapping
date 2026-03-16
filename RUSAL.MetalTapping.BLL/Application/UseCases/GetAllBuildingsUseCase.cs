using RUSAL.MetalTapping.BLL.Application.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.UseCases.Buildings
{
    public class GetAllBuildingsUseCase
    {
        private readonly IGenericRepository<Building> _repository;

        public GetAllBuildingsUseCase(IGenericRepository<Building> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BuildingDto>> ExecuteAsync()
        {
            var buildings = await _repository.GetAllAsync();

            return buildings.Select(b => new BuildingDto
            {
                Id = b.Id,
                Name = b.Name
            });
        }
    }
}