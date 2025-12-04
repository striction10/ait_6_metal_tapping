using RUSAL.MetalTapping.BLL.DTOs;

namespace RUSAL.MetalTapping.BLL.Interfaces
{
    public interface IBuildingService
    {
        Task<IEnumerable<BuildingDto>> GetAllAsync();
        Task<BuildingDto?> GetByIdAsync(int id);
        Task<BuildingDto> CreateAsync(BuildingDto buildingDto);
        Task UpdateAsync(BuildingDto buildingDto);
        Task DeleteAsync(int id);
    }
}
