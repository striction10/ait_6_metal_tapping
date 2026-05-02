using System.ComponentModel.DataAnnotations;

namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Запрос на регистрацию нового пользователя в системе
/// </summary>
/// <param name="email"> Электронная почта пользователя </param>
/// <param name="password"> Пароль пользователя </param>
/// <param name="firstName"> Имя пользователя </param>
/// <param name="lastName"> Фамилия пользователя </param>
/// <param name="role"> Роль пользователя в системе </param>
public record RegisterUserRequest([Required] string email,
    [Required] string password,
    string firstName,
    string lastName, 
    [Required] string role);
