using AutoMapper;
using RUSAL.MetalTapping.BLL.DTOs;
using RUSAL.MetalTapping.BLL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.BLL.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IGenericRepository<Building> _repository;
        private readonly IMapper _mapper;

        public BuildingService(IGenericRepository<Building> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BuildingDto>> GetAllAsync()
        {
            var buildings = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<BuildingDto>>(buildings);
        }
        public async Task<BuildingDto?> GetByIdAsync(int id)
        {
            var building = await _repository.FindByIdAsync(id);
            if (building == null)
            {
                return null;
            }
            return _mapper.Map<BuildingDto>(building);
        }
        public async Task<BuildingDto> CreateAsync(BuildingDto buildingDto)
        {
            var building = await _mapper.Map<Building>(buildingDto);
            _repository.CreateAsync(building);
            return _mapper.Map<BuildingDto>(building);
        }
        public async Task UpdateAsync(BuildingDto buildingDto)
        {
            var building = await _mapper.Map<Building>(buildingDto);
            _repository.UpdateAsync(building);
        }
        public async Task DeleteAsync(int id)
        {
            var building = await _repository.FindByIdAsync(id);
            if (building != null)
            {
                _repository.RemoveAsync(building);
            }
        }
    }
}
