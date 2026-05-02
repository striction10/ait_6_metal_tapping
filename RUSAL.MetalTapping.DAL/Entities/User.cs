using RUSAL.MetalTapping.DAL.Interfaces;

namespace RUSAL.MetalTapping.DAL.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ICollection<UserRoleMembers>  UserRoleMembers { get; set; } = new List<UserRoleMembers>();
    public ICollection<WorkGroupMembers> WorkGroupMembers { get; set; } = new List<WorkGroupMembers>();
}
