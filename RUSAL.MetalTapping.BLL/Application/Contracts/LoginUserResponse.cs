namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Ответ с токеном доступа при успешной авторизации.
/// </summary>
public class LoginResponse
{
    public required string Token { get; set; }
}
