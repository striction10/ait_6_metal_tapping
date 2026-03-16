using System.ComponentModel.DataAnnotations;

namespace RUSAL.MetalTapping.BLL.Application.Contracts
{
    public record LoginUserRequest([Required]string email, [Required]string password);
}