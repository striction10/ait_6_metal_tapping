using System.ComponentModel.DataAnnotations;

namespace RUSAL.MetalTapping.BLL.Contracts
{
    public record LoginUserRequest([Required]string email, [Required]string password);
}