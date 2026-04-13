using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class ShiftAssigmentService
    {
        private readonly IShiftRepository _shiftRepository;

        public ShiftAssigmentService(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task AssignTaskAsync()
        {
            var shift = await _shiftRepository.GetCurrentShift();

        }
    }
}
