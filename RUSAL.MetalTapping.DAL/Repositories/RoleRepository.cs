using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role, RoleModel>, IRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoleRepository(AppDbContext context, IMapper mapper) 
            : base(context, mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            var entity = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);

            return _mapper.Map<Role>(entity);
        }

        public async Task<IEnumerable<Role?>> GetUserRolesAsync(Guid userId)
        {
            var entities = await _context.UserRoleMembers
                .Where(x => x.UserId == userId)
                .Select(x => x.Role)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Role>>(entities);
        }
    }
}