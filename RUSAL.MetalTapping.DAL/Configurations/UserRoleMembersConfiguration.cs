using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class UserRoleMembersConfiguration : IEntityTypeConfiguration<UserRoleMembers>
{
    public void Configure(EntityTypeBuilder<UserRoleMembers> builder)
    {
        builder.ToTable("UserRoleMembers");

        builder.HasKey(urm => new { urm.UserId, urm.RoleId });

        builder.Property(urm => urm.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(urm => urm.RoleId)
            .HasColumnName("RoleId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(urm => urm.User)
            .WithMany(u => u.UserRoleMembers)
            .HasForeignKey(urm => urm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(urm => urm.Role)
            .WithMany(r => r.UserRoleMembers)
            .HasForeignKey(urm => urm.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}