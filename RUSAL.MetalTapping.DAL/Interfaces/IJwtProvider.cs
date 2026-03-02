using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IJwtProvider
    {
        public string GenerateJwtToken(User user);
    }
}