using System.ComponentModel.DataAnnotations;

namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Запрос на авторизацию пользователя
/// </summary>
/// <param name="email"> Электронная почта пользователя </param>
/// <param name="password"> Пароль пользователя </param>
public record LoginUserRequest([Required] string email, [Required] string password);
