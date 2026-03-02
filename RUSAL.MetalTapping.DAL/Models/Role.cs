namespace RUSAL.MetalTapping.DAL.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UserRoleMembers> UserRoleMembers { get; set; } = new List<UserRoleMembers>();
    }
}