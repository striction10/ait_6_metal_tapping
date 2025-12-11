using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class WorkGroupMembersConfiguration : IEntityTypeConfiguration<WorkGroupMembers>
{
    public void Configure(EntityTypeBuilder<WorkGroupMembers> builder)
    {
        builder.ToTable("WorkGroupMembers");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("ID")
            .HasColumnType("numeric(37, 0)")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnType("numeric(37, 0)");

        builder.Property(x => x.WorkGroupId)
            .HasColumnType("numeric(37, 0)");
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.WorkGroupMembers)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
            
        builder.HasOne(x => x.WorkGroup)
            .WithMany(x => x.WorkGroupMembers)
            .HasForeignKey(x => x.WorkGroupId)
            .IsRequired();
    }
}