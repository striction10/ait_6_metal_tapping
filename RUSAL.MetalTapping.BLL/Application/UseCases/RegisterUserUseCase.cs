using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class RegisterUserUseCase(
    UserService userService,
    RoleService roleService,
    IPasswordHasher passwordHasher,
    UserRoleMembersService userRoleMembersService)
{
    private readonly UserService _userService = userService;
    private readonly RoleService _roleService = roleService;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly UserRoleMembersService _userRoleMembersService = userRoleMembersService;

    /// <summary>
    /// Регистрация пользователя в системе
    /// </summary>
    /// <param name="model"> Данные для регистрации</param>
    /// <exception cref="AlreadyExistsException"> Пользователь уже есть в системе</exception>
    /// <exception cref="NotFoundException"> Роль не найдена в бд </exception>
    public async Task ExecuteAsync(RegisterUserRequest model)
    {
        var existingUser = await _userService.FindByEmailAsync(model.email);
        if (existingUser != null)
            throw new AlreadyExistsException("User already exists");

        var role = await _roleService.GetByNameAsync(model.role);

        var hashedPassword = _passwordHasher.Hash(model.password);

        var user = new UserDto
        {
            Id = Guid.NewGuid(),
            FirstName = model.firstName,
            LastName = model.lastName,
            Email = model.email,
            Password = hashedPassword
        };

        await _userService.CreateAsync(user);

        var userRole = new UserRoleMembersDto
        {
            UserId = user.Id,
            RoleId = role.Id
        };

        await _userRoleMembersService.CreateAsync(userRole);
    }
}