using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class RegisterUserUseCase(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IPasswordHasher passwordHasher,
    IGenericRepository<UserRoleMembers> userRoleRepository)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IGenericRepository<UserRoleMembers> _userRoleRepository = userRoleRepository;

    /// <summary>
    /// Регистрация пользователя в системе
    /// </summary>
    /// <param name="model"> Данные для регистрации</param>
    /// <exception cref="AlreadyExistsException"> Пользователь уже есть в системе</exception>
    /// <exception cref="NotFoundException"> Роль не найдена в бд </exception>
    public async Task ExecuteAsync(RegisterUserRequest model)
    {
        var existingUser = await _userRepository.GetByEmailAsync(model.email);
        if (existingUser != null)
            throw new AlreadyExistsException("User already exists");

        var role = await _roleRepository.GetByNameAsync(model.role);
        if (role == null)
            throw new NotFoundException($"Role {model.role} not found");

        var hashedPassword = _passwordHasher.Hash(model.password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = model.firstName,
            LastName = model.lastName,
            Email = model.email,
            Password = hashedPassword
        };

        await _userRepository.CreateAsync(user);

        var userRole = new UserRoleMembers
        {
            UserId = user.Id,
            RoleId = role.Id
        };

        await _userRoleRepository.CreateAsync(userRole);
    }
}