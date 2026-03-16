using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces
{
    public interface IJwtProvider
    {
        public string GenerateJwtToken(User user, IEnumerable<string> roles);
    }
}