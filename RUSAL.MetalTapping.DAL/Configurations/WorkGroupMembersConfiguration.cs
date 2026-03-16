using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class WorkGroupMembersConfiguration : IEntityTypeConfiguration<WorkGroupMembersModel>
{
    public void Configure(EntityTypeBuilder<WorkGroupMembersModel> builder)
    {
        builder.ToTable("WorkGroupMembers");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uniqueidentifier");

        builder.Property(x => x.WorkGroupId)
            .HasColumnName("WorkGroupId")
            .HasColumnType("uniqueidentifier");
        
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