using Microsoft.EntityFrameworkCore;

namespace RUSAL.MetalTapping.DAL.Entities
{
    [Keyless]
    public class WorkGroupMembers
    {
        public int UserId { get; set; }
        public int WorkGroupId { get; set; }
    }
}
