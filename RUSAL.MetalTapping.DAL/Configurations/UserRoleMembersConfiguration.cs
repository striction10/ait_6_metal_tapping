using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class UserRoleMembersConfiguration : IEntityTypeConfiguration<UserRoleMembers>
{
    public void Configure(EntityTypeBuilder<UserRoleMembers> builder)
    {
        builder.ToTable("UserRoleMembers");
        
        builder.HasKey(r => r.UserId);
        
        builder.Property(r => r.UserId)
            .HasColumnName("ID")
            .HasColumnType("numeric(37, 0)")
            .IsRequired();

        builder.Property(r => r.RoleId)
            .HasColumnType("numeric(37, 0)");
        
        builder.Property(b => b.UserId)
            .HasColumnType("numeric(37, 0)");
        
        builder.HasOne(r => r.Role)
            .WithMany(r => r.UserRoleMembers)
            .HasForeignKey(r => r.RoleId)
            .IsRequired();
        
        builder.HasOne(r => r.User)
            .WithMany(r => r.UserRoleMembers)
            .HasForeignKey(r => r.UserId)
            .IsRequired();
    }
}