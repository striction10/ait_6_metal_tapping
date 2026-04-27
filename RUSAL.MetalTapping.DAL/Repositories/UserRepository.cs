using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Entities;
using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories;

public class UserRepository(
    AppDbContext context, IMapper mapper) 
        : GenericRepository<UserDto, User>(context, mapper), 
        IUserRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto?> GetByEmailAsync(string email)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        return _mapper.Map<UserDto>(entity);
    }
}