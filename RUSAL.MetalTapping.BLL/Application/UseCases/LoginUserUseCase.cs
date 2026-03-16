using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.Exceptions;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.UseCases
{
    public class LoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRoleRepository _roleRepository;

        public LoginUserUseCase(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _roleRepository = roleRepository;
        }

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
}