using System.ComponentModel.DataAnnotations;
namespace RUSAL.MetalTapping.BLL.Application.Contracts;

public record RegisterUserRequest([Required] string email, [Required] string password, string firstName, string lastName, [Required] string role);