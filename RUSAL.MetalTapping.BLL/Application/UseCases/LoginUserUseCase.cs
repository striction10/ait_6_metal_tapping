using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class LoginUserUseCase(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider,
    IRoleRepository roleRepository)
{
    private readonly IUserRepository _userRepository = userRepository;
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
        var user = await _userRepository.GetByEmailAsync(model.email);
        if (user == null)
            throw new NotFoundException("User with that email does not found");

        var valid = _passwordHasher.Verify(model.password, user.Password);
        if (!valid)
            throw new AuthentificationException("Invalid login attempt");

        var roles = await _roleRepository.GetUserRolesAsync(user.Id);
        
        var roleNames = roles.Select(r => r.Name);

        return _jwtProvider.GenerateJwtToken(user, roleNames);
    }
}