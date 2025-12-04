using Microsoft.EntityFrameworkCore;

namespace RUSAL.MetalTapping.DAL.Entities
{
    [Keyless]
    public class UserRoleMembers
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
