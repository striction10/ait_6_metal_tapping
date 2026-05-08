using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис для работы с пользователями.
/// </summary>
/// <param name="userRepository">Репозиторий пользователей.</param>
/// <param name="mapper">Маппер объектов.</param>
public class UserService(
    IUserRepository userRepository,
    IMapper mapper)
{
    /// <summary>
    /// Получение пользователя по адресу почты.
    /// </summary>
    /// <param name="email"> Адрес почты пользователя. </param>
    /// <returns> DTO пользователя. </returns>
    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var entity = EnsureFound(
            await userRepository.GetByEmailAsync(email),
            $"User with email {email} was not found");

        return mapper.Map<UserDto>(entity);
    }

    /// <summary>
    /// Поиск записи о пользователе по адресу почты.
    /// </summary>
    /// <param name="email"> Адрес почты. </param>
    /// <returns> DTO пользователя. </returns>
    public async Task<UserDto?> FindByEmailAsync(string email)
    {
        var entity = await userRepository.GetByEmailAsync(email);

        return entity == null
            ? null
            : mapper.Map<UserDto>(entity);
    }

    /// <summary>
    /// Создание записи о новом пользователе в базе данных.
    /// </summary>
    /// <param name="dto"> DTO пользователя. </param>
    public async Task CreateAsync(UserDto dto)
    {
        var entity = mapper.Map<User>(dto);

        await userRepository.CreateAsync(entity);
    }

    /// <summary>
    /// Получения списка всех пользователей системы.
    /// </summary>
    /// <returns>DTO пользователей.</returns>
    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var entities = await userRepository.GetAllAsync();

        return mapper.Map<IEnumerable<UserDto>>(entities);
    }

    /// <summary>
    /// Удаление пользователя из системы.
    /// </summary>
    /// <param name="dto">DTO пользователя.</param>
    public async Task DeleteAsync(UserDto dto)
    {
        var entity = mapper.Map<User>(dto);

        await userRepository.DeleteAsync(entity);
    }

    /// <summary>
    /// Получение пользователя по идентификатору.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> DTO пользователя. </returns>
    public async Task<UserDto?> GetByIdAsync(Guid userId)
    {
        var entity = EnsureFound(
            await userRepository.GetByIdAsync(userId),
            $"User {userId} was not found");

        return mapper.Map<UserDto?>(entity);
    }
}
