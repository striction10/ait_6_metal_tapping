using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
using RUSAL.MetalTapping.DAL.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class LoginUserUseCase(
    UserService userService,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider,
    IRoleRepository roleRepository)
{
    private readonly UserService _userService = userService;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IRoleRepository _roleRepository = roleRepository;

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="model"> Данные для авторизации </param>
    /// <returns> Токен авторизованного пользователя </returns>
    /// <exception cref="NotFoundException"> Пользователя нет в системе </exception>
    /// <exception cref="AuthentificationException"> Неверный пароль </exception>
    public async Task<string> ExecuteAsync(LoginUserRequest model)
    {
        var user = await _userService.GetByEmailAsync(model.email);

        var valid = _passwordHasher.Verify(model.password, user.Password);
        if (!valid)
            throw new AuthentificationException("Invalid login attempt");

        var roles = await _roleRepository.GetUserRolesAsync(user.Id);
        
        var roleNames = roles.Select(r => r.Name);

        return _jwtProvider.GenerateJwtToken(user, roleNames);
    }
}