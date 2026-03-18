using Microsoft.EntityFrameworkCore;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.DAL.Contexts;
using RUSAL.MetalTapping.DAL.Models;
using AutoMapper;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.DAL.Repositories
{
    public class ExternalDataRepository : GenericRepository<ExternalData, ExternalDataModel>, IExternalDataRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ExternalDataRepository(AppDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExternalData?> GetExternalDataWithPotId(Guid id)
        {
            var entity = await _context.ExternalDatas
                .Include(ed => ed.Pot)
                .FirstOrDefaultAsync(ed => ed.PotId == id);

            return _mapper.Map<ExternalData>(entity);
        }
    }
}