using RUSAL.MetalTapping.BLL.Contracts;
using RUSAL.MetalTapping.BLL.Exceptions;
using RUSAL.MetalTapping.DAL.Interfaces;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.BLL.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _provider;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IPasswordHasher passwordHasher, IJwtProvider provider, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _passwordHasher = passwordHasher;
            _provider = provider;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task Register(RegisterUserRequest model)
        {
            var existingUser = await _userRepository.GetByEmailAsync(model.email);

            if (existingUser != null)
            {
                throw new AlreadyExistsException("User already exists");
            }

            var existingRole = await _roleRepository.GetByNameAsync(model.role);

            var hashedPassword = _passwordHasher.Hash(model.password);

            if (existingRole != null)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.firstName,
                    LastName = model.lastName,
                    Email = model.email,
                    Password = hashedPassword,
                    UserRoleMembers = new List<UserRoleMembers>(),
                    WorkGroupMembers = new List<WorkGroupMembers>()
                };

                var userRoleMember = new UserRoleMembers
                {
                    UserId = user.Id,
                    RoleId = existingRole.Id,
                    User = user,
                    Role = existingRole
                };

                user.UserRoleMembers.Add(userRoleMember);

                await _userRepository.CreateAsync(user);
            }
            else
            {
                throw new NotFoundException($"Role {model.role} not found");
            }
        }

        public async Task<string> Login(LoginUserRequest model)
        {
            var existingUser = await _userRepository.GetByEmailAsync(model.email);

            if (existingUser == null)
            {
                throw new NotFoundException("User with that email does not found");
            }

            var result = _passwordHasher.Verify(model.password, existingUser.Password);

            if (!result)
            {
                throw new AuthentificationException("Invalid login attempt");
            }

            var token = _provider.GenerateJwtToken(existingUser);

            return token;
        }
    }
}