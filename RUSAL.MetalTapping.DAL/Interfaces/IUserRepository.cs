using RUSAL.MetalTapping.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUSAL.MetalTapping.DAL.Interfaces
{
    public interface IUserRepository : IGenericService<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}