using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Domain.Interfaces;

public interface IJwtProvider
{
    public string GenerateJwtToken(UserDto userDto, IEnumerable<string> roles);
}