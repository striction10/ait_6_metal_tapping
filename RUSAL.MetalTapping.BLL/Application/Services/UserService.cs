using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;
using static RUSAL.MetalTapping.BLL.Domain.Guard;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Получение пользователя по адресу почты 
    /// </summary>
    /// <param name="email"> Адрес почты пользователя </param>
    /// <returns> DTO пользователя </returns>
    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var entity = EnsureFound(await _userRepository.GetByEmailAsync(email),
            $"User with email {email} was not found");

        return _mapper.Map<UserDto>(entity);
    }

    /// <summary>
    /// Поиск записи о пользователе по адресу почты
    /// </summary>
    /// <param name="email"> Адрес почты </param>
    /// <returns> DTO пользователя </returns>
    public async Task<UserDto?> FindByEmailAsync(string email)
    {
        var entity = await _userRepository.GetByEmailAsync(email);

        return entity == null
            ? null
            : _mapper.Map<UserDto>(entity);
    }

    /// <summary>
    /// Создание записи о новом пользователе в базе данных
    /// </summary>
    /// <param name="dto"> DTO пользователя </param>
    public async Task CreateAsync(UserDto dto)
    {
        var entity = _mapper.Map<User>(dto);

        await _userRepository.CreateAsync(entity);
    }
}