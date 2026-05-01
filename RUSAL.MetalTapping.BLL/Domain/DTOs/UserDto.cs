namespace RUSAL.MetalTapping.BLL.Domain.DTOs;

<<<<<<< HEAD:RUSAL.MetalTapping.BLL/Domain/DTOs/UserDto.cs
public class UserDto
=======
public class UserDto : IDomain
>>>>>>> 974648740c605b4385210bc1637415077b7990a5:RUSAL.MetalTapping.BLL/Domain/Entities/User.cs
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}