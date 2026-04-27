using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.ToTable("Shift");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
        
        builder.Property(b => b.BeginDate)
            .HasColumnName("BeginDate")
            .HasColumnType("datetime")
            .IsRequired();
        
        builder.Property(b => b.EndDate)
            .HasColumnName("EndDate")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(b => b.WorkGroupId)
            .HasColumnName("WorkGroupId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(b => b.BuildingId)
            .HasColumnName("BuildingId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(b => b.WorkGroup)
            .WithMany(b => b.Shifts)
            .HasForeignKey(b => b.WorkGroupId)
            .IsRequired();

        builder.HasOne(b => b.Building)
            .WithMany(b => b.Shifts)
            .HasForeignKey(b => b.BuildingId)
            .IsRequired();
    }
}