using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class PotParametersRepository : GenericRepository<PotParameters, PotParameterModel>, IPotParametersRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PotParametersRepository(AppDbContext context, IMapper mapper) 
            : base(context, mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PotParameters>> GetPotParametersWithGroupId(Guid id)
        {
            var entities = await _context.PotParameters
                .Include(pp => pp.Group)
                .Where(pp => pp.Group.Id == id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PotParameters>>(entities);
        }
    }
}
